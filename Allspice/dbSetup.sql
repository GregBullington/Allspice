CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE recipes(  
    id int NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'Primary Key',
    title VARCHAR(255) NOT NULL,
    subTitle VARCHAR(255) NOT NULL,
    category VARCHAR(255) NOT NULL,
    creatorId VARCHAR(255) NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) DEFAULT CHARSET UTF8 COMMENT '';

CREATE TABLE ingredients(
    id int NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'Primary Key',
    name VARCHAR(255) NOT NULL,
    quantity VARCHAR(255) NOT NULL,
    recipeId int NOT NULL,
    FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) DEFAULT CHARSET UTF8 COMMENT '';

CREATE TABLE steps(
    id int NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'Primary Key',
    iOrder int NOT NULL,
    body TEXT NOT NULL,
    recipeId int NOT NULL,
    FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) DEFAULT CHARSET UTF8 COMMENT '';

CREATE TABLE favorites(  
    id int NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'Primary Key',
    accountId VARCHAR(255) NOT NULL,
    recipeId INT NOT NULL,

    FOREIGN KEY (accountId)
        REFERENCES accounts(id)
        ON DELETE CASCADE,

    FOREIGN KEY (recipeId)
        REFERENCES recipes(id)
        ON DELETE CASCADE

) DEFAULT CHARSET UTF8 COMMENT '';