#!/bin/bash
post_install() {
  if ! id backupuser &>/dev/null; then
    useradd -r -s /usr/bin/nologin -d /nonexistent backupuser
  fi

  install -d -m750 -o backupuser -g backupuser /media/dockedbackup
  install -d -m750 -o backupuser -g backupuser /var/lib/dockedbackup

  systemctl daemon-reload

  cat <<EOF

To register a device for automatic backup, run:
  sudo dockedbackup register <device>

To unregister a device, run:
  sudo dockedbackup unregister <device>

EOF
}


  pre_remove() {
  systemctl disable dockedbackup@*.service 2>/dev/null || :
  systemctl daemon-reload
}
