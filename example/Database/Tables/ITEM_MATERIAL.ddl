DROP TABLE IF EXISTS `ITEM_MATERIAL`;
CREATE TABLE `ITEM_MATERIAL` (
	`item_id` varchar(50) NOT NULL,
	`name_id` varchar(255) NOT NULL,
	PRIMARY KEY (`item_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci