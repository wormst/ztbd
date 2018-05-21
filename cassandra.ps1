docker container rm -f n1 n2 n3
docker run --name=n1 -d -p 9042:9042 tobert/cassandra -dc DC1 -rack RAC1

Start-Sleep -s 30

docker run --name=n2 -d tobert/cassandra -dc DC1 -rack RAC2 -seeds 172.17.0.2

Start-Sleep -s 30

docker run --name=n3 -d tobert/cassandra -dc DC2 -rack RAC1 -seeds 172.17.0.2

Start-Sleep -s 30

docker cp createTables.cql n1:/home
docker exec -it n1 cqlsh -f /home/createTables.cql

Start-Sleep -s 10

docker exec -it n1 nodetool status demo