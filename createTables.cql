CREATE KEYSPACE demo WITH replication = {'class':'NetworkTopologyStrategy', 'DC1':2, 'DC2':1};

USE demo;

CREATE TABLE IF NOT EXISTS people (id uuid PRIMARY KEY, name varchar, surname varchar, city varchar, country varchar, birthday timestamp, telephone varchar, bloodtype varchar, weight double, height double);
CREATE TABLE IF NOT EXISTS bloodAmounts (id uuid PRIMARY KEY, type varchar, amount double);

quit;