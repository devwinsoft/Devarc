DROP TABLE IF EXISTS `CHARACTER`;
CREATE TABLE `CHARACTER` (
	`character_id` INT NOT NULL,
	`charName` varchar(255) NOT NULL,
	`age` INT NOT NULL,
	`gender` varchar(255) NOT NULL,
	PRIMARY KEY (`character_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
