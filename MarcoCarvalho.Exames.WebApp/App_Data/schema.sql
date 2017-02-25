CREATE TABLE pacientes(
	id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    idade INT NOT NULL
);

CREATE TABLE exames(
	id INT AUTO_INCREMENT PRIMARY KEY,
	afericao INT NOT NULL,
	data_hora datetime NOT NULL,
	id_paciente INT NOT NULL REFERENCES pacientes(id)
);