#!/usr/bin/env bash
set -e

# 1. Nologin-Shell ermitteln
if [ -x /usr/bin/nologin ]; then
  NOLOGIN_SHELL=/usr/bin/nologin
elif [ -x /usr/sbin/nologin ]; then
  NOLOGIN_SHELL=/usr/sbin/nologin
else
  NOLOGIN_SHELL=/bin/false
fi

if ! id backupuser &>/dev/null; then
  if command -v useradd &>/dev/null; then
    useradd --system --no-create-home --shell "$NOLOGIN_SHELL" --home-dir /nonexistent backupuser
  elif command -v adduser &>/dev/null; then
    adduser --system --quiet --no-create-home --shell "$NOLOGIN_SHELL" --home /nonexistent --group backupuser
  else
    echo "Fehler: Weder useradd noch adduser gefunden." >&2
    exit 1
  fi
fi

for dir in /media/dockedbackup /var/lib/dockedbackup; do
  install -d -m 750 -o backupuser -g backupuser "$dir"
done

if command -v systemctl &>/dev/null; then
  systemctl daemon-reload
fi

cat <<'EOF'

To register a device for automatic backup, run:
  sudo dockedbackup register <device>

To unregister a device, run:
  sudo dockedbackup unregister <device>

EOF

if command -v systemctl &>/dev/null; then
  systemctl disable dockedbackup@*.service 2>/dev/null || :
  systemctl daemon-reload
fi