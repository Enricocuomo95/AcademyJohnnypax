use PrestitiLibreria;

drop table if exists Libro;
drop table if exists Utente;
drop table if exists Prestito;

create table Libro(
	idLibro int identity(1,1),
	titolo varchar(255) not null,
	annoPubblicazione int not null,
	isDisponibile bit not null,
	primary key (idLibro)
);

--vogliamo tenere lo storicodi tutti i clienti 
--la variabile isDeleted ci dice se la tupla è stata cancellata 
--se è stato elliminato voglio sapere quando è stato elliminato, quindi è di tipo date
create table Utente(
	idUtente int identity(1,1), --da traccia: dovrebbe rappresentare un codice tesseraBiblioteca
	nome varchar(20) not null,
	cognome varchar(20) not null,
	email varchar(20) not null,
	isDeleted date default null,
	primary key(idUtente)
);

--se l'utente è elliminato dal sistema rimane in memoria
--se ellimino il libro devo cancellare anche le vecchie prenotazioni
create table Prestito(
	idPrestito int identity(1,1),
	libroRif int not null,
	utenteRif int not null,
	dataInizio date,
	dataFine date,
	primary key (idPrestito),
	foreign key (libroRif) references Libro(idLibro) on delete cascade,
	foreign key (utenteRif) references Utente(idUtente),
	unique (libroRif,utenteRif)
);

-- Popolamento della tabella Libro
INSERT INTO Libro (titolo, annoPubblicazione, isDisponibile) VALUES ('Il Signore degli Anelli', 1954, 1);
INSERT INTO Libro (titolo, annoPubblicazione, isDisponibile) VALUES ('1984', 1949, 1);
INSERT INTO Libro (titolo, annoPubblicazione, isDisponibile) VALUES ('Cronache di Narnia', 1950, 0);

-- Popolamento della tabella Utente
INSERT INTO Utente (nome, cognome, email) VALUES ('Mario', 'Rossi', 'mario@email.com');
INSERT INTO Utente (nome, cognome, email) VALUES ('Giulia', 'Bianchi', 'giulia@email.com');
INSERT INTO Utente (nome, cognome, email) VALUES ('Luca', 'Verdi', 'luca@email.com');

-- Popolamento della tabella Prestito
INSERT INTO Prestito (libroRif, utenteRif, dataInizio, dataFine) VALUES (1, 1, '2024-04-20', '2024-04-30');
INSERT INTO Prestito (libroRif, utenteRif, dataInizio, dataFine) VALUES (2, 2, '2024-04-22', '2024-05-05');
INSERT INTO Prestito (libroRif, utenteRif, dataInizio, dataFine) VALUES (1, 3, '2024-04-25', '2024-05-10');


