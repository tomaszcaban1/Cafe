# Domain Aggregates

## Menu

```csharp
class Menu
{
    Menu Create();
}
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "hostId": "00000000-0000-0000-0000-000000000000",
    "name": "Awesome menu",
    "description": "The best many all over the world!",  
    "sections": [
        {
            "id": "00000000-0000-0000-0000-000000000000",
            "name": "Lunch",
            "description": "Traditional Polish Lunch",
            "items": [
                {
                    "id": "00000000-0000-0000-0000-000000000000",
                    "name": "Schnitzel",
                    "description": "Traditional Polish Pork Schnitzel"
                    "price": {
                        "amount": 29.99,
                        "currency": "PLN"
                    }
                }
            ]
        }
    ],
    "menuReviewIds": [
        {
			"id": "00000000-0000-0000-0000-000000000000",
			"rating": 10,
            "comment": "Awesome menu!"
        }
	],
    "createdDateTime": "2023-12-31T00:00:00.0000000Z",
    "updatedDateTime": "2023-12-31T00:00:00.0000000Z"
}
```