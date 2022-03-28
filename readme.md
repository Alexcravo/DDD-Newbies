# Newbies Rewards Backend

## Estrutura do projeto

- Idioma do projeto : Inglês
- Padrão de organização : DDD
- Padrão da nomeclatura : CamelCase
- Biblioteca de testes : Padrão C#
- Biblioteca de commitlint : Husky


## Como rodar o projeto

### Iniciar o docker com o banco de dados
#### Para iniciar o docker no linux
```sh
sudo service docker start
```
```sh
docker-compose up -d
```
#### Para iniciar o docker no windows
- Inicia o docker desktop
```sh
docker-compose up -d
```

#### Após iniciar, colocar o seguinte comando no console do gerenciador de pacotes, para atualizar o banco com as tabelas:
```sh
Update-Database
```
### Criar as Migrations
#### Para criar uma migration
- Abrir o console do Gerenciador de pacotes
- Mudar o projeto padrão para Database
- Digitar os seguintes comandos no console do gerenciador de pacotes
```sh
Add-Migration Initial
Update-Database
```

#### Caso queria fazer uma alteração/criação de uma entidade, apagar tudo dentro da pasta das migrations, apagar a tabela e rodar os comandos acima.

### Para rodar o projeto

- Definir o projeto Api como item de inicialização
- Rodar

### Guia para gerenciamento das branches

#### Mudar para  a branch develop
```sh
git checkout develop
```
#### Criar a branch que será desenvolvida
```sh
git checkout -b <nome-da-branch>
```
#### Subir o código para o git
```sh
git push -u origin <nome-da-branch>
```

#### Exemplo
```sh
git checkout develop
git checkout -b endpoint-cadastro
git push -u origin endpoint-cadastro
```


### Colaboradores
- Ygor
- Alex
- Renan
- Dhiulia
- Isadora
- Wallace

# English
# Newbies Rewards Backend

## Project structure

- Project language : English
- Organization pattern : DDD
- Naming pattern : CamelCase
- Test library : Standard C#
- Commitlint library : Husky

## How to run the project

### Starting docker with the database
#### To start docker on linux
```sh
sudo service docker start
```
```sh
docker-compose up -d
```
#### To start docker in windows
- Start docker desktop
```sh
docker-compose up -d
```

#### After starting, put the following command in the package manager console, to update the database with the tables:
```sh
Update-Database
```
### Creating Migrations
#### To create a migration
- Open the Package Manager console
- Change the default project to Database
- Type the following commands in the package manager console
```sh
Add-Migration Initial
Update-Database
```

#### If you want to make a change/create an entity, delete everything inside the migrations folder, delete the table and run the above commands.

### To run the project

- Set the Api project as the startup item
- Run

### Branch management guide

#### Switch to the develop branch
```sh
git checkout develop
```
#### Create the branch to be developed
```sh
git checkout -b <branch-name>
```
#### Upload the code to git
```sh
git push -u origin <branch-name>
```

#### Example
```sh
git checkout develop
git checkout -b endpoint-registration
git push -u origin endpoint-registration
```

### Collaborators
- Ygor
- Alex
- Renan
- Dhiulia
- Isadora
- Wallace
