# RedisUtf8

A minimal API that stores and retrieves user profiles in Redis as raw UTF-8 bytes.

## What it demonstrates

With `System.String`, the data path is:

```
UTF-8 JSON --> string (UTF-16) --> Encoding.GetBytes (UTF-8) --> Redis --> GetString (UTF-16) --> UTF-8 JSON
```

Two transcodes per field on write, two on read.

With Glot, the data stays UTF-8 the entire way:

```
UTF-8 JSON --> Text (UTF-8) --> SerializeToUtf8Bytes --> Redis --> Deserialize<byte[]> --> Text (UTF-8) --> UTF-8 JSON
```

- `TextJsonConverter` reads JSON string values directly from the UTF-8 byte stream — no string allocation
- `JsonSerializer.SerializeToUtf8Bytes` writes `Text` fields as UTF-8 via `TextJsonConverter.Write`
- Redis stores the raw UTF-8 bytes — no encoding conversion
- Cross-encoding search: a UTF-16 query string is compared against UTF-8 stored data using the ASCII fast path

## Prerequisites

Docker (or a Redis instance running on `localhost:6379`).

```bash
docker run -d --name glot-redis -p 6379:6379 redis:alpine
```

## Run

```bash
dotnet run
```

## Endpoints

### `POST /profiles/{id}`

Stores a user profile as UTF-8 JSON bytes in Redis.

```bash
curl -s -X POST http://localhost:5000/profiles/42 \
  -H "Content-Type: application/json" \
  -d '{
    "firstName": "Jane",
    "lastName": "Doe",
    "email": "jane@example.com",
    "bio": "Software engineer who loves distributed systems",
    "company": "Acme Inc",
    "website": "https://jane.dev"
  }'
```

### `GET /profiles/{id}`

Retrieves a profile — UTF-8 bytes from Redis deserialized back to `Text`.

```bash
curl -s http://localhost:5000/profiles/42
```

Returns:

```json
{"firstName":"Jane","lastName":"Doe","email":"jane@example.com","bio":"Software engineer who loves distributed systems","company":"Acme Inc","website":"https://jane.dev"}
```

### `GET /profiles/{id}/contains?q={query}`

Searches within the `bio` field. The query arrives as a UTF-16 string (from the query parameter), but `Text.Contains` handles cross-encoding comparison without transcoding the stored UTF-8 data.

```bash
curl -s "http://localhost:5000/profiles/42/contains?q=distributed"
```

Returns:

```json
{"query":"distributed","found":true}
```

## Configuration

| Variable | Default | Description |
|----------|---------|-------------|
| `Redis` | `localhost:6379` | Redis connection string |
| `--urls` | `http://localhost:5000` | Listen address |

```bash
Redis=my-redis-host:6379 dotnet run --urls http://localhost:5050
```

## Cleanup

```bash
docker rm -f glot-redis
```
