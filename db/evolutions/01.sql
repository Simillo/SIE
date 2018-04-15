create schema security;

create table security.person (
	id int primary key not null,
	name varchar not null,
	cpf varchar not null,
	email varchar not null,
	institution varchar,
	birth_date timestamp not null,
	sex int not null,
	password varchar not null
);

create table security.user (
	id int not null primary key,
	person_id int not null REFERENCES security.person(id),
	access_key varchar not null
);


drop SCHEMA security CASCADE;
