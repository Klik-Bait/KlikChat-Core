# Klik-Core

High level architecture and requirements

Not all cases are covered for now

### Discalimer:
* Authentication block is not implemented yet
* Redis is storing messages for 2 days, (TTL 2 days)

# Message Flow Diagram

```mermaid
graph TD
    A[Sender API Consumer] -->|User sends message| B(API Gateway)
    B -->|Forwards message| C[MessageCore.ClientReceiver]
    C -->|Checks if receiver is active| X{Receiver user is Active?}
    X -->|Yes| D[(RabbitMQ)]
    X -->|No| N[Redis: Store message]

    subgraph Authentication
        H[Receiver User Logs In] -->|Validates Credentials| AS[Authentication Service]
    end

    AS -->|Notify| C
    C -->|Checks redis for messages| N
    N -->|Found messages| D
    D -->|Consumed by| E[MessageCore.ClientSender]
    E -->|Sends message| F(API Gateway)
    F -->|Delivers message| G[Receiver Consumer: Online]
```

## Sender send a massage and receiver is online at the time of sending message 


* User sends message
* Api gateway route it to Message Handler microservice
* Message Handler check if user is online (in this case it is)
* send message to rabbitmq
* Microservice "Message Sender" Consume message
* Message is ack if Receiver API get message otherwise retry, (case when he log in for second and be unavaialbe again for few hours is not covered) we need to implement dead queue letter automation or move later to kafka

## Sender send a massage and receiver is offline at the time of sending message but shorter tha reddis TTL (for now 5 hours)

* User sends message
* Api gateway route it to Message Handler microservice
* Message Handler check if user is online (in this case it is not)
* Save message to redis
* Worker checks if users messages that are stored in redis are active now
* If user is active then message is send to rabbitmq
* Microservice "Message Sender" Consume message
* Message is ack if Receiver API get message otherwise retry, (case when he log in for second and be unavaialbe again for few hours is not covered) we need to implement dead queue letter automation or move later to kafka

