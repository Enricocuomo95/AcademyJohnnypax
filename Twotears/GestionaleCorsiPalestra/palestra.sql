use Palestra;

create table Utente(
	username varchar(255) primary key,
	password_utente varchar(255) not null
);

create table Corso(
	nome varchar(50) primary key,
	codice varchar(255) not null unique,
	descrizione varchar(255) not null,
	tipo_corso varchar(50) not null,
	data_corso date not null,
	ora_inizio varchar(10) not null,
	ora_fine varchar(10) not null,
	n_posti int not null,
	check(tipo_corso in ('principiante','esperto','avanzato'))
);

create table Prenota(
	username_utente varchar(255) not null,
	nome_corso varchar(50) not null,
	foreign key (username_utente) references Utente(username),
	foreign key (nome_corso) references Corso(nome),
	primary key(username_utente, nome_corso)
);

insert into Corso (nome, codice, descrizione, tipo_corso, data_corso, ora_inizio, ora_fine, n_posti) values
('pilates','12345','è troppo belloooo wagliuuu marò che corso!','principiante','30/03/2024','10:00','15:00',20),
('box','123456','è troppo belloooo wagliuuu marò che corso!','principiante','30/04/2024','10:00','15:00',20);

select * from Corso;
select * from Utente;