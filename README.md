# DotNet_Communications_MassTransit_RabbitMQ
This app demonstrates a 'light-weight' Enterprise Service Bus implementation using MassTransit for connecting distributed systems

---

Developed with Visual Studio 2015 Community

---

####Scenario

For this exercise the sender sends a 'Command' message (a new customer record) to a central client, who in turn processes the new customer and generates an 'Event' message that is cascaded out to other clients (sales, management, etc...)

---
####Sending, receiving and observing 'command' messages

Run the 'MassTransit.Client' project and then run the 'MassTransit.Publisher' project.

---

####Sending, receiving 'event' messages

Run the 'MassTransit.Client', 'MassTransit.Client.Sales', 'MassTransit.Client.Management' projects and then run the 'MassTransit.Publisher' project.

---

####Fault Handling

To Test Fault Handling, uncomment line 33 in 'MassTransit.Client.RegisterCustomService' class and execute the 'RunTransitFaultPublisher' method from the 'MassTransit.Publisher.Program' class.

---

###Techs
|Tech|
|----|
|C#|
|RabbitMQ|
|MassTransit|
|Unity|

---

###Features
|Feature|
|-------|
| Command & Event message types |
| Publishing strongly typed messages |
| Dependency Injection|
| Retry policies |
| Fault handling |
|Observing received messages with 'IReceiveObserver' |

---

###Project Layout
|Project|Description|
|-------|-----------|
|MassTransit.Company| Contains our message contracts (command & event), a dummy repository and connection properties class |
|MassTransit.Publisher| Responisble for publishing messages to all receivers |
|MassTransit.Client.(?)| Clients who receive and handle the messages |

---

###Message Observers

Although we only lookedt 'IReceiveObserver' implementation, there are more observers that could be utilized.

|Observer|Type|Description|
|--------|----|-----------|
|ISendObserver|Publisher-based||
|IPublishObserver|Publisher-based||
|IReceiveObserver|Consumer-based|intercept any received message of any concrete type|
|IConsumeObserver|Consumer-based| intercept any consumed message of any concrete type |
|IConsumeMessageObserver(T)|Consumer-based|generic version of IConsumeObserver to intercept specific message types consumed|

---

###Resources
|Title|Author|Website|
|-----|------|-------|
|[MassTransit](http://docs.masstransit-project.com/en/latest/index.html)| | MassTransit |
|[Messaging through a service bus in .NET using MassTransit](https://dotnetcodr.com/2016/09/08/messaging-through-a-service-bus-in-net-using-masstransit-part-1-foundations/) | Andras Nemes | dotnetcodr.com |
|[RabbitMQ Client & Development Tools](https://www.rabbitmq.com/devtools.html)| | RabbitMQ |
