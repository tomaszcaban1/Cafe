# Domain Aggregates

## Order

```csharp
class Order
{
    Order Create(Order order);
}
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "hostId": "00000000-0000-0000-0000-000000000000",
    "userId": "00000000-0000-0000-0000-000000000000",
    "items": [
        {
            "menuItemid": "00000000-0000-0000-0000-000000000000"
        }
    ],
    "price": {
        "amount": 10.99,
        "currency": "PLN"
    },
    "status": "InProgress"
    "createdDateTime": "2020-01-01T00:00:00.0000000Z",
    "updatedDateTime": "2020-01-01T00:00:00.0000000Z"
}
```