#!/bin/bash
set -e

DEVICE="/dev/$1"
MOUNT_BASE="/media/dockedbackup"
MOUNT_POINT="$MOUNT_BASE/$1"

echo "INFO: Detected device $DEVICE"
sleep 2

echo "INFO: Creating mount point $MOUNT_POINT"
mkdir -p "$MOUNT_POINT"

echo "INFO: Mounting $DEVICE to $MOUNT_POINT"
if ! mount "$DEVICE" "$MOUNT_POINT"; then
  echo "ERROR: Failed to mount $DEVICE" >&2
  exit 1
fi

echo "INFO: Invoking dockedbackup process on $MOUNT_POINT"
/usr/bin/dockedbackup process "$MOUNT_POINT"
RC=$?

if [ $RC -ne 0 ]; then
  echo "ERROR: dockedbackup process exited with code $RC" >&2
else
  echo "INFO: dockedbackup process completed successfully"
fi

echo "INFO: Unmounting $MOUNT_POINT"
if ! umount "$MOUNT_POINT"; then
  echo "ERROR: Failed to unmount $MOUNT_POINT" >&2
  exit 1
fi

echo "INFO: Finished handling $DEVICE"
exit 0