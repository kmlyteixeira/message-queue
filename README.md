
### Message Queue
---
Message queue is a form of __asynchronous service-to-service communication__. Messages are stored on the queue until they are processed and deleted and each message is processed only once, by a single worker (or consumer). 

### :pushpin: Some important terms

__1. Worker or Consumer__

A component that receives and processes messages from the message queue

__2. Sender or Producer__

A component that creates and sends messages to the message queue

__3. FIFO (First-In, First-Out)__

The principle where the first message to enter the queue is the first to be processed

__4. Message Time-To-Live__

Defines how long a message can remain in the queue before being deleted

__5. Message broker__

An intermediary software component that facilitates communication between different systems or different threads within the same system. Ex: Azure Service Bus, Apache Kafka and RabbitMQ

### :question: Why use queues? 

:hourglass_flowing_sand: _Long processing time_

When there are tasks that require a significant amount of time to complete, put it in a queue allow the system to continue operating as usual. 

:collision: _Actions prone to failure_

When an action has a high probability of failure, put it in a queue can be a good strategy for handling these failures asynchronously. Ex: If the action fails, it can be placed back in the queue to retry later without broke the normal flow of the system. 

---
### :sparkles: Hands on!
This simple project simulates a messaging service using __Azure Service Bus__. Here you'll find:
```
message-queue-project
|
├── sender
│   ├── sender.ts
|   ├── .env-example
|   ├── package-lock.json
│   └── package.json
|
├── worker
│   ├── Program.cs
│   ├── ServiceBusMessageProcessor.cs
|   ├── appsettings-example.json
|   ├── messages.txt
│   └── worker.csproj
|
└── README.md
```
### :file_folder: __SENDER__

Create and send messages to the queue. 

#### :hammer: Built with _Node v.18_ and _Typescript_

#### :newspaper: Uses [Spaceflight News API](https://www.spaceflightnewsapi.net/) to retrieve articles and create a message

#### :runner: Installing and Running

After create your Service Bus queue (you'll find more here: [Azure Service Bus | Microsoft Learn](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-quickstart-portal)):
1.  Clone this repo `https://github.com/kmlyteixeira/message-queue.git`
2.  Enter in the sender folder:  `cd sender`
3.  Use the `.env-example` file to assign your _connection string_ and _queue name_
4.  Run `npm install` to install the dependencies 
5.  Run `npm start`
6.  The console should display this message: __All articles sent successfully!__
7.  Track this on the Azure Portal

### :file_folder: __WORKER__

Receives and processes messages from the queue.

#### :hammer: Built with _.NET 8_ 

#### :runner: Installing and Running

After create your Service Bus queue and clone this repo:

1.  Enter in the worker folder:  `cd worker`
2.  Use the `.appsettings-example.json` file to assign your _connection string_ and _queue name_
3.  Run `dotnet build worker.csproj` to build this project
4.  Run `dotnet run worker.csproj`
5.  The app will process the active messages in the queue and wait for new messages (track this on console)
6.  Incoming messages will be written to the `messages.txt` file
7.  Track this on the Azure Portal

---
### :books: __Learn more__
:one: [Quickstart - Use Azure Service Bus queues from .NET app - Azure Service Bus | Microsoft Learn](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dotnet-get-started-with-queues?tabs=passwordless)

:two: [Create a Queue Service - .NET | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/extensions/queue-service)

:three: [Introduction to Message Queuing. What is a Message Queue? | by Kabilesh Kumararatnam | Medium](https://medium.com/tech-sauce/introduction-to-message-queuing-4a7ab8968b59)

:four: [FIFO (computing and electronics)](https://en.wikipedia.org/wiki/FIFO_(computing_and_electronics))

:five: [Learn Queue data structures in 10 minutes 🎟️ - YouTube](https://www.youtube.com/watch?v=nqXaPZi99JI)

:six: [Queues in 3 minutes - Youtube](https://www.youtube.com/watch?v=D6gu-_tmEpQ)
