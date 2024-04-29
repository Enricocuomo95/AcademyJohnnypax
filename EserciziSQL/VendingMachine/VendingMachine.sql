use VendingMachine;

drop table if exists Effettua;
drop table if exists Tecnico;
drop table if exists Transazione;
drop table if exists Prodotto;
drop table if exists Fornitore;
drop table if exists Distributore;


create table Distributore(
	id int identity(1,1),
	modello varchar(255) not null,
	posizione varchar(100) not null,
	primary key(id)
);

create table Fornitore(
	id int identity(1,1),
	nome varchar(50),
	telefono varchar(10),
	primary key (id)
);

create table Prodotto(
	id int identity(1,1),
	id_fornitore int not null,
	nome varchar(50) unique not null,
	prezzo int not null,
	n_store int default 0,
	primary key (id),
	foreign key (id_fornitore) references Fornitore(id)
);

create table Transazione(
	id_distributore int not null,
	id_prodotto int not null,
	data_transazione datetime not null,
	importo int not null,
	primary key(id_distributore,id_prodotto),
	foreign key(id_distributore) references Distributore(id),
	foreign key(id_prodotto) references Prodotto(id)
);

create table Tecnico(
	cod_fis char(18) primary key,
	nome varchar(50) not null,
);

create table Effettua(
	id_distributore int not null,
	id_tecnico char(18) not null,
	tipo_intervento varchar(255),
	data_intervento datetime not null,
	primary key (id_distributore,id_tecnico),
	foreign key (id_distributore) references Distributore(id),
	foreign key (id_tecnico) references Tecnico(cod_fis)
);


-- Istanze per la tabella Distributore
INSERT INTO Distributore (modello, posizione) VALUES ('Modello1', 'Posizione1');
INSERT INTO Distributore (modello, posizione) VALUES ('Modello2', 'Posizione2');
INSERT INTO Distributore (modello, posizione) VALUES ('Modello3', 'Posizione3');

-- Istanze per la tabella Fornitori
INSERT INTO Fornitore (nome, telefono) VALUES ('Fornitore1', '1234567890');
INSERT INTO Fornitore (nome, telefono) VALUES ('Fornitore2', '0987654321');
INSERT INTO Fornitore (nome, telefono) VALUES ('Fornitore3', '5555555555');

-- Istanze per la tabella Prodotto
INSERT INTO Prodotto (id_fornitore, nome, prezzo, n_store) VALUES (1,'Prodotto1', 100, 10);
INSERT INTO Prodotto (id_fornitore, nome, prezzo, n_store) VALUES (2,'Prodotto2', 200, 20);
INSERT INTO Prodotto (id_fornitore, nome, prezzo, n_store) VALUES (3,'Prodotto3', 300, 30);

-- Istanze per la tabella Transazione
INSERT INTO Transazione (id_distributore, id_prodotto, data_transazione, importo) VALUES (1, 1, CONVERT(datetime, '2024-04-24 10:00:00', 120), 100);
INSERT INTO Transazione (id_distributore, id_prodotto, data_transazione, importo) VALUES (2, 2, CONVERT(datetime, '2024-04-25 11:00:00', 120), 200);
INSERT INTO Transazione (id_distributore, id_prodotto, data_transazione, importo) VALUES (3, 3, CONVERT(datetime, '2024-04-26 12:00:00', 120), 300);

-- Istanze per la tabella Tecnico
INSERT INTO Tecnico (cod_fis, nome) VALUES ('ABC12345678901234', 'Tecnico1');
INSERT INTO Tecnico (cod_fis, nome) VALUES ('DEF12345678901234', 'Tecnico2');
INSERT INTO Tecnico (cod_fis, nome) VALUES ('GHI12345678901234', 'Tecnico3');

-- Istanze per la tabella Effettua
INSERT INTO Effettua (id_distributore, id_tecnico, tipo_intervento, data_intervento) VALUES (1, 'ABC12345678901234', 'Intervento1', CONVERT(datetime, '2024-04-24 13:00:00', 120));
INSERT INTO Effettua (id_distributore, id_tecnico, tipo_intervento, data_intervento) VALUES (2, 'DEF12345678901234', 'Intervento2', CONVERT(datetime, '2024-04-25 14:00:00', 120));
INSERT INTO Effettua (id_distributore, id_tecnico, tipo_intervento, data_intervento) VALUES (3, 'GHI12345678901234', 'Intervento3', CONVERT(datetime, '2024-04-26 15:00:00', 120));

--Creare una vista ProductsByVendingMachine che mostri tutti i prodotti disponibili in ciascun
--distributore, includendo l'ID e la posizione del distributore, il nome del prodotto, il prezzo e la
--quantità disponibile

create view ProductsByVendingMachine as 
	select d.id as distributore, d.posizione, p.nome as 'nome prodotto', p.prezzo,p.n_store 
	from Distributore d inner join Transazione t on d.id = t.id_distributore
		inner join Prodotto p on t.id_prodotto = p.id;

select * from ProductsByVendingMachine;

--Generare una vista RecentTransactions che elenchi le ultime transazioni effettuate, mostrando
--l'ID della transazione, la data/ora, il distributore, il prodotto acquistato e l'importo della
--transazione.

create view RecentTransactions as
	select t.id_distributore as Distributore, t.id_prodotto as Prodotto, t.data_transazione as 'Data Transazione', t.importo as Importo, p.nome as 'Nome prodotto'
	from Distributore d inner join Transazione t on d.id = t.id_distributore
		inner join Prodotto p on t.id_prodotto = p.id;

select * from RecentTransactions order by('Data Transazione');

--Creare una vista ScheduledMaintenance che mostri tutti i distributori che hanno una
--manutenzione programmata, includendo l'ID e la posizione del distributore e la data dell'ultima e
--della prossima manutenzione.
drop view if exists ManutenzioniFuture;
drop view if exists ScheduledMaintenance;

create view ManutenzioniFuture as 
	select d.id, e.data_intervento
	from Distributore d inner join Effettua e on d.id = e.id_distributore
	where e.data_intervento > CURRENT_TIMESTAMP

create view ScheduledMaintenance as
	SELECT TOP 2 *
	FROM ManutenzioniFuture m INNER JOIN Effettua e ON m.id = e.id_distributore
	ORDER BY e.data_intervento;


--Implementare una stored procedure RefillProduct che consenta di aggiungere scorte di un
--prodotto specifico in un distributore, richiedendo l'ID del distributore, l'ID del prodotto e la
--quantità da aggiungere

CREATE PROCEDURE RefillProduct 
	@IdDistributore int,
	@IdProdotto int,
	@quantità int
AS
	BEGIN 
		BEGIN TRY
			BEGIN TRANSACTION
				DECLARE @prezzo int

				SELECT @prezzo = p.prezzo 
				FROM Prodotto p 
				WHERE p.id = @IdProdotto;

				SELECT * 
				FROM Distributore d
				WHERE d.id = @IdDistributore;

				INSERT INTO Transazione (id_distributore, id_prodotto, data_transazione, importo) values
				(@IdDistributore, @IdProdotto, CURRENT_TIMESTAMP, (@quantità * @prezzo));

				UPDATE Prodotto
				SET n_store = n_store + @quantità
				WHERE id = @IdProdotto;
			COMMIT TRANSACTION
		END TRY
		BEGIN CATCH
			PRINT 'ERRORE'
			ROLLBACK
		END CATCH
	END

EXEC RefillProduct @IdDistributore = 1,
					@IdProdotto = 1,
					@quantità = 100;


--Creare una stored procedure ScheduleMaintenance per programmare un intervento di
--manutenzione su un distributore, specificando l'ID del distributore e la data della manutenzione.
CREATE PROCEDURE ScheduleMaintenance 
	@IdDistributore int,
	@IdTecnico varchar,
	@data DATETIME,
	@TipoIntervento varchar
AS
	BEGIN 
		BEGIN TRY
			BEGIN TRANSACTION

				SELECT * 
				FROM Distributore d
				WHERE d.id = @IdDistributore;

				SELECT *
				FROM Tecnico t
				WHERE t.cod_fis = @IdTecnico;

				INSERT INTO Effettua (id_distributore, id_tecnico, tipo_intervento, data_intervento) VALUES 
				(@IdDistributore, @IdTecnico, @TipoIntervento, @data);
			COMMIT TRANSACTION
		END TRY
		BEGIN CATCH
			PRINT 'ERRORE'
			ROLLBACK
		END CATCH
	END

EXEC ScheduleMaintenance @IdDistributore = 2,
						@IdTecnico = 3,
						@data = '2024-04-24 10:00:00',
						@TipoIntervento = 'bisogna cambiare le manopole del gelato';


--Implementare una stored procedure UpdateProductPrice che permetta di aggiornare il prezzo di
--un prodotto specifico, richiedendo l'ID del prodotto e il nuovo prezzo

CREATE PROCEDURE UpdateProductPrice 
	@IdProdotto int,
	@Prezzo int
AS
	BEGIN
		SELECT * 
		FROM Prodotto p 
		WHERE p.id = @IdProdotto;

		UPDATE Prodotto  
		SET prezzo = prezzo + @Prezzo
		WHERE id = @IdProdotto;

	END

EXEC UpdateProductPrice @IdProdotto = 1,
						@Prezzo = 10;

SELECT * FROM Prodotto;
