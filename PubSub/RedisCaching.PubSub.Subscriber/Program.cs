using StackExchange.Redis;

// creating a connection to the our redis database using StackExchange.Redis library
ConnectionMultiplexer connection = await ConnectionMultiplexer.ConnectAsync("localhost:1200");

// Creating a subscriber using connection that we created above (subscriber => consumer, publish => publisher)
var subscriber = connection.GetSubscriber();

// Subscribing the channel that we specified in our publisher 
await subscriber.SubscribeAsync("mychannel.*", (channel, message) =>
{
    Console.WriteLine($"Message: {message}");
});

Console.Read();
