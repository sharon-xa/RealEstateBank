using System.Globalization;
using System.Text.RegularExpressions;

using Npgsql;

using RealEstateBank.Utils.Exceptions;

namespace RealEstateBank.Helpers;

public static class DbErrors {
    private static readonly IReadOnlyDictionary<string, int> StatusBySqlState =
        new Dictionary<string, int> {
            [PostgresErrorCodes.UniqueViolation] = StatusCodes.Status409Conflict,                       // 23505
            [PostgresErrorCodes.ForeignKeyViolation] = StatusCodes.Status409Conflict,                   // 23503
            [PostgresErrorCodes.NotNullViolation] = StatusCodes.Status422UnprocessableEntity,           // 23502
            [PostgresErrorCodes.CheckViolation] = StatusCodes.Status422UnprocessableEntity,             // 23514
            [PostgresErrorCodes.ExclusionViolation] = StatusCodes.Status409Conflict,                    // 23P01
            [PostgresErrorCodes.DeadlockDetected] = StatusCodes.Status503ServiceUnavailable,            // 40P01
            [PostgresErrorCodes.SerializationFailure] = StatusCodes.Status409Conflict,                  // 40001
            [PostgresErrorCodes.StringDataRightTruncation] = StatusCodes.Status422UnprocessableEntity,  // 22001
            ["22P02"] = StatusCodes.Status422UnprocessableEntity,                                       // InvalidTextRepresentation
            [PostgresErrorCodes.InvalidDatetimeFormat] = StatusCodes.Status422UnprocessableEntity       // 22007
        };

    public static DatabaseException GetDbException(PostgresException ex, string repository, string method) {

        var table = Humanize(ex.TableName) ?? "record";
        var column = Humanize(ex.ColumnName) ?? "field";
        var message = BuildMessage(ex, table, column);

        var statusCode = StatusBySqlState.TryGetValue(ex.SqlState, out var code)
            ? code
            : StatusCodes.Status500InternalServerError;

        return new DatabaseException(
            message: message,
            repository: repository,
            method: method,
            statusCode: statusCode,
            sqlState: ex.SqlState
        );
    }

    private static string BuildMessage(PostgresException ex, string table, string column) {
        switch (ex.SqlState) {
            case var c when c == PostgresErrorCodes.UniqueViolation:
                return $"{Singularize(table)} already exists.";
            case var c when c == PostgresErrorCodes.ForeignKeyViolation:
                return $"The referenced {ExtractReferencedTable(ex.Detail) ?? "record"} was not found for {column} in {table}.";
            case var c when c == PostgresErrorCodes.NotNullViolation:
                return $"{column} is required.";
            case var c when c == PostgresErrorCodes.CheckViolation:
                return $"Invalid value for {column}.";
            case var c when c == PostgresErrorCodes.ExclusionViolation:
                return $"Conflicts with an existing {Singularize(table)}.";
            case var c when c == PostgresErrorCodes.DeadlockDetected:
                return "A database deadlock occurred. Please retry.";
            case var c when c == PostgresErrorCodes.SerializationFailure:
                return "The request conflicted with a concurrent update. Please retry.";
            case var c when c == PostgresErrorCodes.StringDataRightTruncation:
                return $"{column} is too long.";
            case "22P02":
                return $"Invalid format for {column}.";
            case var c when c == PostgresErrorCodes.InvalidDatetimeFormat:
                return $"Invalid date/time format for {column}.";
            default:
                return "A database error occurred.";
        }
    }

    private static string? ExtractReferencedTable(string? detail) {
        if (string.IsNullOrWhiteSpace(detail)) return null;
        var m = Regex.Match(detail, @"is not present in table ""([^""]+)""", RegexOptions.IgnoreCase);
        return m.Success ? Humanize(m.Groups[1].Value) : null;
    }

    private static string? Humanize(string? ident) {
        if (string.IsNullOrWhiteSpace(ident)) return ident ?? null;
        var s = ident.Replace('_', ' ').Trim();
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s);
    }

    private static string Singularize(string name) {
        var esRules = new HashSet<string> { "ch", "sh", "x", "s", "z" };

        if (name.EndsWith("ies")) {
            return name.Substring(0, name.Length - 3) + "y";
        }
        else if (name.EndsWith("es")) {
            var noEs = name.Substring(0, name.Length - 2);

            var lastTwoChars = noEs.Substring(noEs.Length - 2);
            if (esRules.Contains(lastTwoChars))
                return name.Substring(0, name.Length - 2);

            var lastChar = noEs.Substring(noEs.Length - 1);
            if (esRules.Contains(lastChar))
                return name.Substring(0, name.Length - 2);

            return name;
        }
        else if (name.EndsWith("s")) {
            return name.Substring(0, name.Length - 1);
        }
        else {
            return name;
        }
    }
}
