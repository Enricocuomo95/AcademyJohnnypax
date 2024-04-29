use MarioKart;
drop table if exists Personaggio;
drop table if exists Giocatore;

create table Giocatore(
	GiocatoreID int identity(1,1),
	Username varchar(255) unique not null,
	Nominativo varchar(255) not null,
	Passward varchar(255) not null,
	primary key (GiocatoreID)
);

create table Personaggio(
	PersonaggioID int identity(1,1),
	GiocatoreRIF int,
	Nome varchar(255) unique not null,
	Costo char(1) not null,
	Categoria varchar(3) not null,
	Disponibile bit default 1,
	check (Costo in ('1','2','3','4')),
	check (Categoria in ('50','100','150')),
	primary key(PersonaggioID),
	foreign key(GiocatoreRif) references Giocatore(GiocatoreID)
);

insert into Personaggio (Nome, Costo, Categoria) values
('Rambo','4','150'),
('Brus Waine','3','150'),
('Chuck Norris','4','150'),
('Jon Waine','3','150'),
('Bender','2','50'),
('MarioBro','2','100'),
('Sonic','2','100'),
('Casper','1','50');


select * from Giocatore