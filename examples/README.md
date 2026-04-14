# Examples

Real-world examples demonstrating Glot's zero-transcode, zero-alloc text pipeline.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (or .NET 8+ with TFM adjustment)
- [Docker](https://docs.docker.com/get-docker/) (for Redis example)

## HttpPassthrough

A minimal API that receives a JSON webhook, builds a normalized payload using `TextBuilder`, and forwards it to a downstream service. The entire text pipeline stays UTF-8 — no transcoding to UTF-16 at any point.

**What it demonstrates:**

- `Text` fields deserialized from UTF-8 JSON without string allocation
- `TextBuilder` with same-encoding `Append` (memcpy, zero transcode)
- `OwnedText.Create` interpolated string handler (zero intermediate string)

**Run:**

```bash
cd examples/Glot.Examples.HttpPassthrough
dotnet run
```

The app includes a built-in `/echo` endpoint that acts as the downstream service — no external dependencies needed.

**Try it:**

```bash
# TextBuilder pipeline
curl -s -X POST http://localhost:5000/forward \
  -H "Content-Type: application/json" \
  -d '{"firstName":"Jane","lastName":"Doe","email":"jane@example.com","street":"123 Main St","city":"Portland","country":"US","phone":"+1-555-0100","company":"Acme Inc"}'

# Interpolated string handler
curl -s -X POST http://localhost:5000/forward-interpolated \
  -H "Content-Type: application/json" \
  -d '{"firstName":"Jane","lastName":"Doe","email":"jane@example.com","street":"123 Main St","city":"Portland","country":"US","phone":"+1-555-0100","company":"Acme Inc"}'
```

To use a different port or point the downstream client elsewhere:

```bash
DownstreamUrl=http://localhost:5050 dotnet run --urls http://localhost:5050
```

## RedisUtf8

A minimal API that stores and retrieves user profiles in Redis as raw UTF-8 bytes. The data path is `UTF-8 JSON -> Text (UTF-8) -> Redis (UTF-8 bytes) -> Text (UTF-8) -> UTF-8 JSON` with no UTF-16 intermediate.

**What it demonstrates:**

- `JsonSerializer.SerializeToUtf8Bytes` with `TextJsonConverter` — writes `Text` fields as UTF-8 directly
- Redis stores raw UTF-8 bytes — no string encoding overhead
- Cross-encoding search: UTF-8 stored data searched with a UTF-16 query string

**Start Redis:**

```bash
docker run -d --name glot-redis -p 6379:6379 redis:alpine
```

**Run:**

```bash
cd examples/Glot.Examples.RedisUtf8
dotnet run
```

To connect to a different Redis instance:

```bash
Redis=my-redis-host:6379 dotnet run
```

**Try it:**

```bash
# Store a profile
curl -s -X POST http://localhost:5000/profiles/42 \
  -H "Content-Type: application/json" \
  -d '{"firstName":"Jane","lastName":"Doe","email":"jane@example.com","bio":"Software engineer who loves distributed systems","company":"Acme Inc","website":"https://jane.dev"}'

# Retrieve it
curl -s http://localhost:5000/profiles/42

# Search within a profile field (cross-encoding: UTF-8 data, UTF-16 query)
curl -s "http://localhost:5000/profiles/42/contains?q=distributed"
```

**Cleanup:**

```bash
docker rm -f glot-redis
```
