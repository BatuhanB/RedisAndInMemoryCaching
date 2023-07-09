using StackExchange.Redis;

// creating a connection to the our redis database using StackExchange.Redis library
ConnectionMultiplexer connection = await ConnectionMultiplexer.ConnectAsync("localhost:1200");

// Creating a subscriber using connection that we created above (subscriber => consumer, publish => publisher)
var subscriber = connection.GetSubscriber();

// Creating a while loop for publish message to the channel that we named as well as message
while (true)
{
    // Entering channel name
    Console.Write("Channel Name: ");
    string channel = Console.ReadLine();

    // Entering message
    Console.Write("Message: ");
    string message = Console.ReadLine();

    // Our subscriber act like a publisher and it take 2 arguments (channel name , message)
    await subscriber.PublishAsync(channel,message);
}
