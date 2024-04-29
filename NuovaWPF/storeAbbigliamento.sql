use NegozioAbbigliamento;

create table Prodotto(
	idProdotto int identity(1,1),
	marca varchar(20) not null,
	descrizione varchar(255) not null,
	urlImg varchar(50) not null,
	prezzo int not null,
	categoriaProdotto varchar(50),
	primary key (idProdotto)
);

create table Utente(
	idUtente int identity(1,1),
	emailUtente varchar(50) not null,
	passwordUtente varchar(50) not null,
	unique (emailUtente),
	primary key (idUtente)
);

create table Ordine(
	idOrdine int identity(1,1),
	utenteRif int not null,
	dataOrdine Date not null,
	dataConsegna Date not null,
	primary key (idOrdine),
	foreign key (utenteRif) references Utente(idUtente)
);

create table Riferisce(
	ordineRif int not null,
	prodottoRif int, 
	--controllare che ci sia disponibilita o rimuovere ordine
	--non metto il not null qui, questo mi spezza ladipendenza ciclica
	primary key(ordineRif,prodottoRif),
	foreign key(ordineRif) references Ordine(idOrdine),
	foreign key(prodottoRif) references Prodotto(idProdotto)
);

create table Variazione(
	idVariazione int identity(1,1),
	prodottoRif int not null,
	colore varchar(20) not null,
	taglia varchar(3) not null,
	quantita int default 0,
	primary key(idVariazione),
	foreign key(prodottoRif) references Prodotto(idProdotto)
);

create table Offerta(
	idOfferta int identity(1,1),
	variazioneRif int not null,
	dataInizio date not null,
	dataFine date not null,
	prezzoOfferta int not null,
	primary key(idOfferta),
	foreign key(variazioneRif) references Variazione(idVariazione)
);


