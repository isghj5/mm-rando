#!/usr/bin/env bash
# Filter out OoT-specific information from z64rasm

subdir='assembly'

python -m git_filter_repo \
    --path target/oot/ \
    --path target/oot/build \
    --path target/oot/c/ \
    --path target/oot/src/ \
    --path c/ \
    --path build/ \
    --path notes/ \
    --path src/ \
    --invert-paths \
    --to-subdirectory-filter "$subdir" \
    --tag-rename '':"${subdir}-"
