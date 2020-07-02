#!/bin/bash

set -o errexit
set -o nounset
set -o pipefail
set -o xtrace

readonly SCRIPT_DIR="$(cd "$(dirname "$0")"; pwd)"
readonly PROJECT_HOME="${SCRIPT_DIR}/.."
readonly PROJECT_NAME="$(basename "$(cd "${PROJECT_HOME}"; pwd)")"

readonly EVENTS_SOURCE_DIR="${PROJECT_HOME}/Assets/Datas/Events/"
readonly FUNCTIONS_DIR="${PROJECT_HOME}/functions/"
readonly FUNCTIONS_EVENTS_DIR="${PROJECT_HOME}/functions/Events/"

cp -r "${EVENTS_SOURCE_DIR}" "${FUNCTIONS_EVENTS_DIR}"

cd "${FUNCTIONS_DIR}"
yarn install

cd "${PROJECT_HOME}"
yarn install
yarn run firebase deploy --only functions
