using Npgsql;

namespace RealEstateBank.Repository;

public static class DbErrors {
    public static bool IsUniqueViolation(PostgresException pgEx) {
        return pgEx.SqlState == PostgresErrorCodes.UniqueViolation;
    }

    public static bool IsForeignKeyViolation(Exception ex) {
        if (ex is PostgresException pgEx) {
            return pgEx.SqlState == PostgresErrorCodes.ForeignKeyViolation;
        }
        return false;
    }

    public static bool IsNotNullViolation(Exception ex) {
        if (ex is PostgresException pgEx) {
            return pgEx.SqlState == PostgresErrorCodes.NotNullViolation;
        }
        return false;
    }

    public static bool IsDeadlock(Exception ex) {
        if (ex is PostgresException pgEx) {
            return pgEx.SqlState == PostgresErrorCodes.DeadlockDetected;
        }
        return false;
    }

    public static bool IsSerializationFailure(Exception ex) {
        if (ex is PostgresException pgEx) {
            return pgEx.SqlState == PostgresErrorCodes.SerializationFailure;
        }
        return false;
    }

    public static bool IsConnectionException(Exception ex) {
        if (ex is PostgresException pgEx) {
            return pgEx.SqlState.StartsWith("08");
        }
        return false;
    }

    public static bool IsInsufficientResources(Exception ex) {
        if (ex is PostgresException pgEx) {
            return pgEx.SqlState.StartsWith("53");
        }
        return false;
    }

    public static bool IsCriticalFailure(Exception ex) {
        if (ex is PostgresException pgEx) {
            return CriticalFailureCodes.Any(code => pgEx.SqlState.StartsWith(code));
        }
        return false;
    }

    private static readonly string[] CriticalFailureCodes = new[] {
        "53", "57P01", "57P02", "57P03", "58", "F0", "XX"
    };
}
