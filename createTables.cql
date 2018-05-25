CREATE KEYSPACE demo WITH replication = {'class':'NetworkTopologyStrategy', 'DC1':2, 'DC2':1};

USE demo;

CREATE TABLE IF NOT EXISTS people (id uuid PRIMARY KEY, name varchar, surname varchar, city varchar, country varchar, birthday timestamp, telephone varchar, bloodtype varchar, weight double, height double);
CREATE TABLE IF NOT EXISTS cities (id uuid PRIMARY KEY, name varchar, aPlus double, aMinus double, bPlus double, bMinus double, abPlus double, abMinus double, oPlus double, oMinus double); 

CONSISTENCY LOCAL_ONE;

quit;