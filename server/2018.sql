/*
Navicat MySQL Data Transfer

Source Server         : mysql
Source Server Version : 50717
Source Host           : localhost:3306
Source Database       : 2018

Target Server Type    : MYSQL
Target Server Version : 50717
File Encoding         : 65001

Date: 2018-07-13 20:04:45
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `model_player`
-- ----------------------------
DROP TABLE IF EXISTS `model_player`;
CREATE TABLE `model_player` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `userId` bigint(20) unsigned NOT NULL DEFAULT '0',
  `kindId` smallint(6) NOT NULL DEFAULT '0',
  `name` varchar(50) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `country` smallint(6) unsigned DEFAULT '0',
  `rank` smallint(6) unsigned NOT NULL DEFAULT '0',
  `level` smallint(6) unsigned NOT NULL DEFAULT '1',
  `experience` int(20) unsigned NOT NULL DEFAULT '0',
  `attackValue` smallint(6) unsigned NOT NULL,
  `defenceValue` smallint(6) unsigned NOT NULL,
  `walkSpeed` smallint(6) unsigned NOT NULL,
  `attackSpeed` decimal(2,1) unsigned NOT NULL,
  `attackScope` smallint(6) unsigned NOT NULL,
  `hp` smallint(6) unsigned NOT NULL,
  `mp` smallint(6) unsigned NOT NULL,
  `maxHp` smallint(6) unsigned NOT NULL,
  `maxMp` smallint(6) unsigned NOT NULL,
  `serverId` smallint(6) unsigned NOT NULL DEFAULT '0',
  `areaId` smallint(6) unsigned NOT NULL DEFAULT '0',
  `x` int(10) unsigned NOT NULL DEFAULT '0',
  `y` int(10) unsigned NOT NULL DEFAULT '0',
  `skillPoint` smallint(6) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `INDEX_GAME_NAME` (`name`),
  KEY `INDEX_PALYER_USER_ID` (`userId`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of model_player
-- ----------------------------

-- ----------------------------
-- Table structure for `model_player_type`
-- ----------------------------
DROP TABLE IF EXISTS `model_player_type`;
CREATE TABLE `model_player_type` (
  `id` smallint(6) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(20) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `title` varchar(20) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `hp` smallint(6) unsigned NOT NULL,
  `mp` smallint(6) unsigned NOT NULL,
  `attackValue` smallint(6) unsigned NOT NULL,
  `defenceValue` smallint(6) unsigned NOT NULL,
  `walkSpeed` smallint(6) unsigned NOT NULL,
  `attackSpeed` decimal(2,1) unsigned NOT NULL,
  `attackScope` smallint(6) unsigned NOT NULL,
  `introduce` varchar(500) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of model_player_type
-- ----------------------------
INSERT INTO `model_player_type` VALUES ('1', 'mage', '法师', '100', '100', '10', '10', '300', '0.5', '500', '可以学习各类法术，大范围杀伤');
INSERT INTO `model_player_type` VALUES ('2', 'warrior', '战士', '150', '80', '20', '20', '300', '0.7', '300', '可以学习各种战技，接近对手，给予致命一击');
INSERT INTO `model_player_type` VALUES ('3', 'warlock', '术士', '80', '100', '5', '5', '400', '0.5', '500', '最神秘的种族，可以掌握封印，控制');
INSERT INTO `model_player_type` VALUES ('4', 'pastor', '牧师', '100', '100', '10', '10', '350', '0.5', '300', '掌握生命的奥义，提供各类增益，减益效果');

-- ----------------------------
-- Table structure for `model_user`
-- ----------------------------
DROP TABLE IF EXISTS `model_user`;
CREATE TABLE `model_user` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `password` varchar(50) COLLATE utf8_unicode_ci DEFAULT '',
  `contact` varchar(25) COLLATE utf8_unicode_ci DEFAULT NULL,
  `loginCount` smallint(6) unsigned DEFAULT '0',
  `lastLoginIp` varchar(25) COLLATE utf8_unicode_ci DEFAULT NULL,
  `lastLoginTime` date DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `INDEX_ACCOUNT_NAME` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of model_user
-- ----------------------------
INSERT INTO `model_user` VALUES ('1', 'hans', '1', null, '18', '192.168.2.137', '2018-07-13');
