  #!/bin/bash
  systemctl disable dockedbackup@*.service 2>/dev/null || :
  systemctl daemon-reload