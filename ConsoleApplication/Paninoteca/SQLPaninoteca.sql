use panino;


create table Panino(
	Nome varchar(250) primary key,
	Descrizione varchar(250),
	Prezzo float not null,
	Vegan bit not null
	);

	insert into Panino (Nome, Descrizione, Prezzo, Vegan) values
		('pippo','panino buono', 10,0);

		