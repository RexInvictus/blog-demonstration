#!/bin/bash
set -e

# Set AWS credentials
export LITESTREAM_ACCESS_KEY_ID="AWS_ACCESS_KEY"
export LITESTREAM_SECRET_ACCESS_KEY="AWS_SECRET_KEY"

# Set the replica URL
export REPLICA_URL="s3://hikingblogdatabase/"

# Restore the database if it does not already exist.
if [ -f /app/hikingblog.db ]; then
    echo "Database already exists, skipping restore"
else
    echo "No database found, restoring from replica if exists"
    litestream restore -v -if-replica-exists -o /app/hikingblog.db "${REPLICA_URL}"
fi

exec litestream replicate -exec "dotnet api.dll"
