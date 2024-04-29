use Universita;

drop table if exists Iscrizione;
drop table if exists Frequentare;
drop table if exists Studente;
drop table if exists Esame;

create table Studente(
	matricola varchar(20) not null,
	nominativo varchar(20) not null,
	primary key (matricola)
);
create table Esame(
	nome_corso varchar(20) not null,
	date_esame date check (date_esame < '2022-01-01'),
	crediti int check (crediti >= 0) not null,
	primary key (nome_corso)
);
create table Iscrizione(
	esameRif varchar(20),
	studenteRif varchar(20),
	data_esame date DEFAULT GETDATE(), --CURRENT_TIMESTAMP,
	foreign key (studenteRif) references Studente (matricola) on delete cascade,
	foreign key (esameRif) references Esame(nome_corso) on delete cascade,
	primary key (studenteRif, esameRif)
);


insert into Studente values
	('0001234','studente1'),
	('0001235','studente2'),
	('0001236','studente3'),
	('0001237','studente4'),
	('0001238','studente5');


insert into Esame (nome_corso,date_esame,crediti) values
	('esame1','2020-03-03',6),
	('esame2','2020-03-03',6),
	('esame3','2020-03-03',6),
	('esame4','2020-03-03',6),
	('esame5','2020-03-03',6);


insert into Iscrizione (esameRif,studenteRif) values
	('esame1','0001234'),
	('esame2','0001234'),
	('esame3','0001234'),
	('esame4','0001236'),
	('esame4','0001235'),
	('esame4','0001237');


		-- stampo tutti gli studenti
select *
	from Studente s left join Iscrizione f on s.matricola = f.studenteRif
		left join Esame e on f.esameRif = e.nome_corso

		-- stampo tutti gli esami
select *
	from Studente s right join Iscrizione f on s.matricola = f.studenteRif
		right join Esame e on f.esameRif = e.nome_corso

		-- stampo tutto
select *
	from Studente s right join Iscrizione f on s.matricola = f.studenteRif
		right join Esame e on f.esameRif = e.nome_corso
union 
	select *
	from Studente s left join Iscrizione f on s.matricola = f.studenteRif
		left join Esame e on f.esameRif = e.nome_corso;

		--satampo tutto con full join
select *
	from Studente s full join Iscrizione f on s.matricola = f.studenteRif
		full join Esame e on f.esameRif = e.nome_corso;


select *
	from Studente s inner join Iscrizione f on s.matricola = f.studenteRif
		inner join Esame e on f.esameRif = e.nome_corso;