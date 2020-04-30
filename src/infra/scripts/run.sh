export GITHUB_USERNAME="$1"
export GITHUB_PASSWORD="$2"

cd src/LeanHub.Console
dotnet run user sync
