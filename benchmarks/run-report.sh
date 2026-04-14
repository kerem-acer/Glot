#!/bin/bash
# Runs all benchmark classes sequentially with --job short.
# Must be run from the project root: ./benchmarks/run-report.sh
#
# Usage:
#   ./benchmarks/run-report.sh                                              # all classes
#   ./benchmarks/run-report.sh --filter '*TextCreation*'                    # single class
#   ./benchmarks/run-report.sh --job medium                                 # override job
#   ./benchmarks/run-report.sh --param:N=256,4096 --param:Locale=Ascii      # filter params
#   ./benchmarks/run-report.sh --anyCategories FromSpan                     # filter category

set -e
PROJECT="benchmarks/Glot.Benchmarks.csproj"

CLASSES=(
    TextCreationBenchmarks
    TextCreationUtf8Benchmarks
    TextSearchBenchmarks
    TextEqualityBenchmarks
    TextMutationBenchmarks
    TextSplitBenchmarks
    TextConcatBenchmarks
    TextInterpolationBenchmarks
    TextBuilderBenchmarks
    LinkedTextBenchmarks
    JsonSerializationBenchmarks
    HttpPipelineBenchmarks
)

EXTRA_ARGS=("$@")

# Default to --job short unless user passed --job
JOB="--job short"
for arg in "${EXTRA_ARGS[@]}"; do
    if [[ "$arg" == "--job" ]]; then
        JOB=""
        break
    fi
done

# If user passed --filter, run only that filter (skip per-class loop)
HAS_FILTER=false
for arg in "${EXTRA_ARGS[@]}"; do
    if [[ "$arg" == "--filter" ]]; then
        HAS_FILTER=true
        break
    fi
done

echo "=== Glot Benchmark Report ==="
echo "Date: $(date -u '+%Y-%m-%d %H:%M UTC')"
echo "Runtime: .NET $(dotnet --version)"
echo ""

if $HAS_FILTER; then
    echo ">>> Running with custom filter..."
    dotnet run --project "$PROJECT" -c Release -- \
        $JOB \
        "${EXTRA_ARGS[@]}"
else
    for CLASS in "${CLASSES[@]}"; do
        echo ">>> Running $CLASS..."
        dotnet run --project "$PROJECT" -c Release -- \
            --filter "*${CLASS}*" \
            $JOB \
            "${EXTRA_ARGS[@]}"
        echo ""
    done
fi

echo "=== Done ==="
