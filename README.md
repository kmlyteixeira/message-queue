## Message Queue

### Main concepts of Message Queue

Message queue is a form of asynchronous service-to-service communication. Messages are stored on the queue until they are processed and deleted and each message is processed only once, by a single worker (or consumer). 

#### :pushpin: Worker or Consumer
A component that receives and processes messages from the message queue

#### :pushpin: Sender or Producer 
A component that creates and sends messages to the message queue

#### :pushpin: FIFO (First-In, First-Out) 
The principle where the first message to enter the queue is the first to be processed

#### :pushpin: Message Time-To-Live
Defines how long a message can remain in the queue before being deleted

#### :pushpin: Message broker 
An intermediary software component that facilitates communication between different systems or different threads within the same system. Ex: Azure Service Bus, Apache Kafka and RabbitMQ

#### :pushpin: Topics

#### :pushpin: Cluster

### :question: Why use queues? 

Long processing time: 
When there are tasks that require a significant amount of time to complete, put it in a queue allow the system to continue operating as usual. 

Actions prone to failure:
When an action has a high probability of failure, put it in a queue can be a good strategy for handling these failures asynchronously. Ex: If the action fails, it can be placed back in the queue to retry later without broke the normal flow of the system. 

