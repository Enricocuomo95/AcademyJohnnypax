use Impiegato;

create table Impiegato(
	impiegato_id int identity(1,1) primary key,
	matricola varchar(255) not null unique,
	nome varchar(20) not null,
	cognome varchar(20) not null,
	data_nascita Date not null,
	ruolo varchar(50) not null,
	reparto varchar(50) not null,
	indirizzo_res varchar(255) not null,
	citta_res varchar(50) not null,
	provincia_res varchar(2) not null,
	check (ruolo in ('impiegato1','metalmeccanico','professionale'))
);


drop table if exists Reparto;
drop table if exists Citta;
drop table if exists Provincia;

create table Reparto(
	reparto_id int identity(1,1) primary key,
	nome_reparto varchar(50) not null
);

create table Provincia(
	provincia_id int identity(1,1) primary key,
	sigla char(2),
	nome varchar(50) not null
);

create table Citta(
	citta_id int identity(1,1) primary key,
	nome_citta varchar(50) not null,
	provincia_rif int not null,
	foreign key (provincia_rif) references Provincia(provincia_id)
);


INSERT INTO Provincia (nome, sigla) VALUES 
('Agrigento', 'AG'),
('Alessandria', 'AL'),
('Ancona', 'AN'),
('Aosta', 'AO'),
('Arezzo', 'AR'),
('Ascoli Piceno', 'AP'),
('Asti', 'AT'),
('Avellino', 'AV'),
('Bari', 'BA'),
('Barletta-Andria-Trani', 'BT'),
('Belluno', 'BL'),
('Benevento', 'BN'),
('Bergamo', 'BG'),
('Biella', 'BI'),
('Bologna', 'BO'),
('Bolzano', 'BZ'),
('Brescia', 'BS'),
('Brindisi', 'BR'),
('Cagliari', 'CA'),
('Caltanissetta', 'CL'),
('Campobasso', 'CB'),
('Carbonia-Iglesias', 'CI'),
('Caserta', 'CE'),
('Catania', 'CT'),
('Catanzaro', 'CZ'),
('Chieti', 'CH'),
('Como', 'CO'),
('Cosenza', 'CS'),
('Cremona', 'CR'),
('Crotone', 'KR'),
('Cuneo', 'CN'),
('Enna', 'EN'),
('Fermo', 'FM'),
('Ferrara', 'FE'),
('Firenze', 'FI'),
('Foggia', 'FG'),
('Forlì-Cesena', 'FC'),
('Frosinone', 'FR'),
('Genova', 'GE'),
('Gorizia', 'GO'),
('Grosseto', 'GR'),
('Imperia', 'IM'),
('Isernia', 'IS'),
('La Spezia', 'SP'),
('Aquila', 'AQ'),
('Latina', 'LT'),
('Lecce', 'LE'),
('Lecco', 'LC'),
('Livorno', 'LI'),
('Lodi', 'LO'),
('Lucca', 'LU'),
('Macerata', 'MC'),
('Mantova', 'MN'),
('Massa-Carrara', 'MS'),
('Matera', 'MT'),
('Medio Campidano', 'VS'),
('Messina', 'ME'),
('Milano', 'MI'),
('Modena', 'MO'),
('Monza e della Brianza', 'MB'),
('Napoli', 'NA'),
('Novara', 'NO'),
('Nuoro', 'NU'),
('Ogliastra', 'OG'),
('Olbia-Tempio', 'OT'),
('Oristano', 'OR'),
('Padova', 'PD'),
('Palermo', 'PA'),
('Parma', 'PR'),
('Pavia', 'PV'),
('Perugia', 'PG'),
('Pesaro e Urbino', 'PU'),
('Pescara', 'PE'),
('Piacenza', 'PC'),
('Pisa', 'PI'),
('Pistoia', 'PT'),
('Pordenone', 'PN'),
('Potenza', 'PZ'),
('Prato', 'PO'),
('Ragusa', 'RG'),
('Ravenna', 'RA'),
('Reggio Calabria', 'RC'),
('Reggio Emilia', 'RE'),
('Rieti', 'RI'),
('Rimini', 'RN'),
('Roma', 'RM'),
('Rovigo', 'RO'),
('Salerno', 'SA'),
('Sassari', 'SS'),
('Savona', 'SV'),
('Siena', 'SI'),
('Siracusa', 'SR'),
('Sondrio', 'SO'),
('Taranto', 'TA'),
('Teramo', 'TE'),
('Terni', 'TR'),
('Torino', 'TO'),
('Trapani', 'TP'),
('Trento', 'TN'),
('Treviso', 'TV'),
('Trieste', 'TS'),
('Udine', 'UD'),
('Varese', 'VA'),
('Venezia', 'VE'),
('Verbano-Cusio-Ossola', 'VB'),
('Vercelli', 'VC'),
('Verona', 'VR'),
('Vibo Valentia', 'VV'),
('Vicenza', 'VI'),
('Viterbo', 'VT');


INSERT INTO Citta (nome_citta, provincia_rif) VALUES 
('Roma', 81),
('Milano', 73),
('Napoli', 47),
('Palermo', 66),
('Genova', 46),
('Bologna', 14),
('Firenze', 38);

INSERT INTO Reparto (nome_reparto) VALUES 
('Vendite'),
('Marketing'),
('Risorse Umane'),
('Produzione');

