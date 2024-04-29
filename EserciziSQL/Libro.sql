use Libreria;

DROP TABLE IF EXISTS Libro;

create table Libro(titolo varchar(20) not null, autore varchar(20), cod_ISBN varchar(13) primary key);

insert into Libro values('titolo1','pippo','12345');
insert into Libro values('titolo2','enrico','12346');
insert into Libro values('titolo3','pippo','12347');

insert into Libro (titolo,autore,cod_ISBN)values
	('titolo1','giovanni','12348'),
	('vinodannata','topolino','12349'),
	('annadaicapellirossi','pippo','12340');


select * from Libro where autore = 'pippo';

select * from Libro where titolo like '%an%';

select * from Libro where cod_ISBN = '12349';