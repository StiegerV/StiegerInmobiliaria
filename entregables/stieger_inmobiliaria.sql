-- phpMyAdmin SQL Dump
-- version 4.7.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 30, 2025 at 05:05 PM
-- Server version: 5.7.17
-- PHP Version: 5.6.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `stieger_inmobiliaria`
--

-- --------------------------------------------------------

--
-- Table structure for table `contrato`
--

CREATE TABLE `contrato` (
  `id_contrato` int(11) NOT NULL,
  `monto` float NOT NULL,
  `fecha_inicio` date NOT NULL,
  `fecha_fin` date NOT NULL,
  `id_inmueble` int(11) NOT NULL,
  `id_inquilino` int(11) NOT NULL,
  `fecha_fin_original` datetime DEFAULT NULL,
  `creado_por` int(11) NOT NULL,
  `terminado_por` int(11) DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1'
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `contrato`
--

INSERT INTO `contrato` (`id_contrato`, `monto`, `fecha_inicio`, `fecha_fin`, `id_inmueble`, `id_inquilino`, `fecha_fin_original`, `creado_por`, `terminado_por`, `activo`) VALUES
(16, 80085, '2025-05-09', '2025-07-09', 7, 9, '2025-12-09 00:00:00', 1, NULL, 1);

-- --------------------------------------------------------

--
-- Table structure for table `inmueble`
--

CREATE TABLE `inmueble` (
  `id_inmueble` int(11) NOT NULL,
  `id_propietario` int(11) NOT NULL,
  `direccion` varchar(80) NOT NULL,
  `uso` enum('comercial','residencial','otro') NOT NULL DEFAULT 'otro',
  `tipo` enum('local','casa','departamento','otro') NOT NULL DEFAULT 'otro',
  `ambientes` int(11) NOT NULL,
  `cordenadas` varchar(50) NOT NULL,
  `precio` double NOT NULL,
  `disponible` enum('disponible','suspendido','otro') NOT NULL DEFAULT 'otro',
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  `imagen` varchar(500) DEFAULT ''
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `inmueble`
--

INSERT INTO `inmueble` (`id_inmueble`, `id_propietario`, `direccion`, `uso`, `tipo`, `ambientes`, `cordenadas`, `precio`, `disponible`, `activo`, `imagen`) VALUES
(1, 4, 'calle siemprevivatest edit', 'residencial', 'casa', 12, '50', 10, 'suspendido', 1, NULL),
(2, 4, 'direccionPrueba', 'comercial', 'local', 1, '1;1', 10.15, 'disponible', 1, NULL),
(3, 6, 'testcrearinmueble', 'otro', 'departamento', 80085, '080085', 80085, 'disponible', 1, '/uploads/638823169200886744.png'),
(7, 13, 'Literally a mountain', 'otro', 'otro', 0, '38;-80', 80085, 'disponible', 1, '/uploads/638823191926381278.png');

-- --------------------------------------------------------

--
-- Table structure for table `inquilino`
--

CREATE TABLE `inquilino` (
  `id_inquilino` int(11) NOT NULL,
  `dni` varchar(250) NOT NULL,
  `nombre` varchar(80) NOT NULL,
  `apellido` varchar(80) NOT NULL,
  `telefono` varchar(250) DEFAULT NULL,
  `mail` varchar(100) DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1'
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `inquilino`
--

INSERT INTO `inquilino` (`id_inquilino`, `dni`, `nombre`, `apellido`, `telefono`, `mail`, `activo`) VALUES
(2, 'dniInquilino2', 'nombreInquilino2', 'apellidoInquilino2', '498433', 'contaco@2', 1),
(3, 'inquilinodni', 'inquilinonombre', 'apellidonombre', '', 'contactoInquilino@dotcom', 1),
(4, 'si', 'si', 'si', 'si', 'si@asd.com', 1),
(5, '44359378', 'pablo', 'power', NULL, NULL, 1),
(6, '146984321', 'pablito', 'weak', NULL, NULL, 1),
(7, '16541321', 'PEPITO', 'TOUCHDOWN', '498433', 'yeahbaby@yeah.com', 1),
(8, '4964323', 'Roberto', 'Estropajo', '1651496843', 'robertito@mail.com', 1),
(9, '987431369', 'Jhon', 'Denver', '346831684321354', 'westVirginia@mail.com', 1);

-- --------------------------------------------------------

--
-- Table structure for table `pago`
--

CREATE TABLE `pago` (
  `id_pago` int(11) NOT NULL,
  `id_contrato` int(11) NOT NULL,
  `monto` double NOT NULL,
  `fecha` date NOT NULL,
  `observacion` varchar(250) DEFAULT NULL,
  `estado` enum('completado','en proceso','fallo','anulado') DEFAULT 'en proceso',
  `creado_por` int(11) NOT NULL,
  `terminado_por` int(11) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `pago`
--

INSERT INTO `pago` (`id_pago`, `id_contrato`, `monto`, `fecha`, `observacion`, `estado`, `creado_por`, `terminado_por`) VALUES
(22, 16, 80085, '2025-11-09', 'Pago 11', 'anulado', 0, NULL),
(21, 16, 80085, '2025-10-09', 'Pago 10', 'anulado', 0, NULL),
(20, 16, 80085, '2025-09-09', 'Pago 9', 'anulado', 0, NULL),
(19, 16, 80085, '2025-08-09', 'Pago 8', 'anulado', 0, NULL),
(18, 16, 80085, '2025-07-09', 'Pago 7', 'en proceso', 0, NULL),
(17, 16, 80085, '2025-06-09', 'Pago 6', 'en proceso', 0, NULL),
(16, 16, 80085, '2025-05-09', 'Pago 5', 'anulado', 0, 1),
(23, 16, 160170, '2025-05-09', 'Multa por cancelacion', 'en proceso', 0, NULL),
(24, 16, 15000, '2025-05-24', 'test pago quien lo creo', 'completado', 2, NULL),
(25, 16, 0, '2025-05-24', NULL, 'anulado', 2, 1),
(26, 16, 651651, '2025-05-28', 'test pago modal', 'completado', 1, NULL),
(27, 16, 1212, '2025-05-21', 'apgo modal 2.0', 'completado', 1, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `propietario`
--

CREATE TABLE `propietario` (
  `id_propietario` int(11) NOT NULL,
  `dni` varchar(250) NOT NULL,
  `nombre` varchar(80) NOT NULL,
  `apellido` varchar(80) NOT NULL,
  `telefono` varchar(250) DEFAULT NULL,
  `mail` varchar(250) DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1'
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `propietario`
--

INSERT INTO `propietario` (`id_propietario`, `dni`, `nombre`, `apellido`, `telefono`, `mail`, `activo`) VALUES
(4, 'dni', 'nombre', 'apellido', 'telefono', NULL, 1),
(6, '44359371', 'ricardo', 'vizcay', '266469420', NULL, 1),
(7, 'dninuevo', 'nombrenuevo', 'apellidonuevo', 'telefononuevo', NULL, 1),
(8, '873214', 'Fabrizio', 'Arias', '6546874321', NULL, 1),
(9, 'nuevo propietrario', 'nuevo propietrario2', 'nuevo propietrario3', 'nuevo propietrario4', NULL, 0),
(10, 'propietario', 'propietario2', 'propietario3', 'propietario4', NULL, 1),
(11, 'dninuevo', 'dninuevo1', 'dninuevo2', 'dninuevo3', 'yeahbaby@yeah.com', 1),
(12, 'dninuevo', 'pablo', 'apellidoedit', '498433', 'yeahbaby@yeah.com', 1),
(13, '169146521084', 'Ernie', 'Ford', '249231648', 'ernieford@16ton.com', 1),
(14, '3169846321', 'kenny', 'loggins', '236832013', 'kennylogins@cruise.com', 0);

-- --------------------------------------------------------

--
-- Table structure for table `usuario`
--

CREATE TABLE `usuario` (
  `id_usuario` int(11) NOT NULL,
  `nombre` varchar(250) NOT NULL,
  `contrasena` varchar(250) NOT NULL,
  `rol` enum('administrador','empleado') NOT NULL,
  `imagen` varchar(500) DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1'
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `usuario`
--

INSERT INTO `usuario` (`id_usuario`, `nombre`, `contrasena`, `rol`, `imagen`, `activo`) VALUES
(1, 'pepe', '5hqBl21VmvDgmqtINSRlPcfZp+Hy0Jh06/rZPco2rYE=', 'administrador', '/uploads/638833658354288170.png', 1),
(2, 'The_fabri_twitch', 'lXaFRl3W4TvMKDV6uubHRSK3p25ixD5MhImsXC7fs1A=', 'empleado', '/uploads/638833674994349034.png', 1);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `contrato`
--
ALTER TABLE `contrato`
  ADD PRIMARY KEY (`id_contrato`),
  ADD KEY `fk_id_inquilino` (`id_inquilino`),
  ADD KEY `fk_id_inmueble` (`id_inmueble`);

--
-- Indexes for table `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`id_inmueble`),
  ADD KEY `fk_id_propietario` (`id_propietario`);

--
-- Indexes for table `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`id_inquilino`);

--
-- Indexes for table `pago`
--
ALTER TABLE `pago`
  ADD PRIMARY KEY (`id_pago`),
  ADD KEY `fk_id_contrato` (`id_contrato`);

--
-- Indexes for table `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`id_propietario`);

--
-- Indexes for table `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`id_usuario`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `contrato`
--
ALTER TABLE `contrato`
  MODIFY `id_contrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;
--
-- AUTO_INCREMENT for table `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `id_inmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;
--
-- AUTO_INCREMENT for table `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `id_inquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
--
-- AUTO_INCREMENT for table `pago`
--
ALTER TABLE `pago`
  MODIFY `id_pago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;
--
-- AUTO_INCREMENT for table `propietario`
--
ALTER TABLE `propietario`
  MODIFY `id_propietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;
--
-- AUTO_INCREMENT for table `usuario`
--
ALTER TABLE `usuario`
  MODIFY `id_usuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
