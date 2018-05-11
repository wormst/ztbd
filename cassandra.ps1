docker run --name n1 -d -p 9042:9042 cassandra:latest


docker exec -it n1 cqlsh

create keyspace demo; with replication = {'class':'SimpleStrategy', 'replication_factor': 1};
use demo;