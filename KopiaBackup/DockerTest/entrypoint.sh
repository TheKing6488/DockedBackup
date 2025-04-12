#!/bin/bash
set -e

# shellcheck disable=SC2145
echo "Execute command: $@"
exec ./KopiaBackup.Console "$@"
