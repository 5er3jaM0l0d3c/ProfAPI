--
-- PostgreSQL database dump
--

-- Dumped from database version 17.4
-- Dumped by pg_dump version 17.4

-- Started on 2025-12-24 09:17:11

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE IF EXISTS "Prof26";
--
-- TOC entry 5026 (class 1262 OID 123412)
-- Name: Prof26; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "Prof26" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';


ALTER DATABASE "Prof26" OWNER TO postgres;

\connect "Prof26"

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4992 (class 0 OID 124313)
-- Dependencies: 218
-- Data for Name: Machine; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Machine" VALUES (1, 'ул. Ленина, 15, офис 1', 1, 'SN00123456', 'INV001001', '2023-01-15', 12, 5000, 8, 1, 1, '2023-01-10', 1, '2022-12-01', '2023-01-10');
INSERT INTO public."Machine" VALUES (2, 'пр. Мира, 25, ТЦ "Центральный"', 2, 'SN00123457', 'INV001002', '2023-02-20', 12, 5000, 10, 1, 2, '2023-02-15', 2, '2022-12-15', '2023-02-15');
INSERT INTO public."Machine" VALUES (3, 'ул. Советская, 8, бизнес-центр "Плаза"', 3, 'SN00123458', 'INV001003', '2023-03-10', 6, 3000, 6, 2, 1, '2023-03-05', 1, '2023-01-20', '2023-03-05');
INSERT INTO public."Machine" VALUES (4, 'пр. Победы, 45, кинотеатр "Синема"', 4, 'SN00123459', 'INV001004', '2023-01-25', 12, 5000, 12, 1, 3, '2023-01-20', 3, '2022-11-30', '2023-01-20');
INSERT INTO public."Machine" VALUES (5, 'ул. Гагарина, 12, университет', 2, 'SN00123460', 'INV001005', '2023-04-05', 6, 3000, 8, 3, 4, '2023-04-01', 4, '2023-02-10', '2023-04-01');
INSERT INTO public."Machine" VALUES (6, 'ул. Кирова, 33, больница', 1, 'SN00123461', 'INV001006', '2023-05-12', 12, 5000, 9, 1, 1, '2023-05-10', 1, '2023-03-15', '2023-05-10');
INSERT INTO public."Machine" VALUES (7, 'пр. Строителей, 7, спорткомплекс', 3, 'SN00123462', 'INV001007', '2023-06-18', 6, 3000, 7, 1, 2, '2023-06-15', 5, '2023-04-20', '2023-06-15');


--
-- TOC entry 4994 (class 0 OID 124325)
-- Dependencies: 220
-- Data for Name: MachineStatus; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."MachineStatus" VALUES (1, 'Активный');
INSERT INTO public."MachineStatus" VALUES (2, 'На обслуживании');
INSERT INTO public."MachineStatus" VALUES (3, 'Неисправен');
INSERT INTO public."MachineStatus" VALUES (4, 'Выведен из эксплуатации');
INSERT INTO public."MachineStatus" VALUES (5, 'Резервный');


--
-- TOC entry 4996 (class 0 OID 124332)
-- Dependencies: 222
-- Data for Name: Machine_Product; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Machine_Product" VALUES (2, 1, 2, 7);
INSERT INTO public."Machine_Product" VALUES (3, 1, 4, 15);
INSERT INTO public."Machine_Product" VALUES (4, 1, 6, 13);
INSERT INTO public."Machine_Product" VALUES (5, 1, 8, 5);
INSERT INTO public."Machine_Product" VALUES (6, 2, 1, 15);
INSERT INTO public."Machine_Product" VALUES (7, 2, 3, 5);
INSERT INTO public."Machine_Product" VALUES (8, 2, 5, 10);
INSERT INTO public."Machine_Product" VALUES (9, 2, 7, 3);
INSERT INTO public."Machine_Product" VALUES (10, 2, 9, 20);
INSERT INTO public."Machine_Product" VALUES (11, 3, 2, 10);
INSERT INTO public."Machine_Product" VALUES (12, 3, 4, 20);
INSERT INTO public."Machine_Product" VALUES (13, 3, 6, 15);
INSERT INTO public."Machine_Product" VALUES (14, 3, 8, 7);
INSERT INTO public."Machine_Product" VALUES (15, 3, 10, 3);
INSERT INTO public."Machine_Product" VALUES (16, 4, 1, 13);
INSERT INTO public."Machine_Product" VALUES (17, 4, 3, 7);
INSERT INTO public."Machine_Product" VALUES (18, 4, 5, 11);
INSERT INTO public."Machine_Product" VALUES (19, 4, 9, 25);
INSERT INTO public."Machine_Product" VALUES (20, 4, 11, 5);
INSERT INTO public."Machine_Product" VALUES (21, 5, 2, 11);
INSERT INTO public."Machine_Product" VALUES (22, 5, 4, 17);
INSERT INTO public."Machine_Product" VALUES (23, 5, 7, 1);
INSERT INTO public."Machine_Product" VALUES (24, 5, 10, 5);
INSERT INTO public."Machine_Product" VALUES (25, 5, 12, 10);
INSERT INTO public."Machine_Product" VALUES (26, 6, 1, 17);
INSERT INTO public."Machine_Product" VALUES (27, 6, 2, 13);
INSERT INTO public."Machine_Product" VALUES (28, 6, 6, 20);
INSERT INTO public."Machine_Product" VALUES (29, 6, 8, 10);
INSERT INTO public."Machine_Product" VALUES (30, 6, 11, 7);
INSERT INTO public."Machine_Product" VALUES (31, 7, 3, 9);
INSERT INTO public."Machine_Product" VALUES (32, 7, 4, 15);
INSERT INTO public."Machine_Product" VALUES (33, 7, 5, 13);
INSERT INTO public."Machine_Product" VALUES (34, 7, 7, 5);
INSERT INTO public."Machine_Product" VALUES (1, 1, 1, 1);
INSERT INTO public."Machine_Product" VALUES (35, 7, 12, 9);


--
-- TOC entry 4998 (class 0 OID 124339)
-- Dependencies: 224
-- Data for Name: Maintenance; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Maintenance" VALUES (1, 1, '2024-01-15 10:30:00', 2);
INSERT INTO public."Maintenance" VALUES (2, 2, '2024-01-20 14:15:00', 3);
INSERT INTO public."Maintenance" VALUES (3, 3, '2024-02-05 09:00:00', 2);
INSERT INTO public."Maintenance" VALUES (4, 4, '2024-02-10 11:45:00', 3);
INSERT INTO public."Maintenance" VALUES (5, 5, '2024-02-15 16:20:00', 2);
INSERT INTO public."Maintenance" VALUES (6, 1, '2024-03-10 08:45:00', 6);
INSERT INTO public."Maintenance" VALUES (7, 6, '2024-03-18 13:30:00', 3);
INSERT INTO public."Maintenance" VALUES (9, 1, '2025-11-07 23:00:11', 1);


--
-- TOC entry 5000 (class 0 OID 124346)
-- Dependencies: 226
-- Data for Name: Maintenance_Problem; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Maintenance_Problem" VALUES (1, 1, 1);
INSERT INTO public."Maintenance_Problem" VALUES (2, 2, 2);
INSERT INTO public."Maintenance_Problem" VALUES (3, 3, 5);
INSERT INTO public."Maintenance_Problem" VALUES (4, 4, 7);
INSERT INTO public."Maintenance_Problem" VALUES (5, 5, 4);
INSERT INTO public."Maintenance_Problem" VALUES (6, 6, 9);
INSERT INTO public."Maintenance_Problem" VALUES (7, 7, 6);
INSERT INTO public."Maintenance_Problem" VALUES (9, 9, 1);


--
-- TOC entry 5002 (class 0 OID 124353)
-- Dependencies: 228
-- Data for Name: Maintenance_WorkDescription; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Maintenance_WorkDescription" VALUES (1, 1, 1);
INSERT INTO public."Maintenance_WorkDescription" VALUES (2, 1, 6);
INSERT INTO public."Maintenance_WorkDescription" VALUES (3, 2, 2);
INSERT INTO public."Maintenance_WorkDescription" VALUES (4, 3, 3);
INSERT INTO public."Maintenance_WorkDescription" VALUES (5, 3, 6);
INSERT INTO public."Maintenance_WorkDescription" VALUES (6, 4, 4);
INSERT INTO public."Maintenance_WorkDescription" VALUES (7, 5, 5);
INSERT INTO public."Maintenance_WorkDescription" VALUES (8, 6, 7);
INSERT INTO public."Maintenance_WorkDescription" VALUES (9, 6, 12);
INSERT INTO public."Maintenance_WorkDescription" VALUES (10, 7, 8);
INSERT INTO public."Maintenance_WorkDescription" VALUES (11, 7, 11);
INSERT INTO public."Maintenance_WorkDescription" VALUES (12, 9, 1);


--
-- TOC entry 5004 (class 0 OID 124360)
-- Dependencies: 230
-- Data for Name: ManufactureCountry; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."ManufactureCountry" VALUES (1, 'Россия');
INSERT INTO public."ManufactureCountry" VALUES (2, 'Китай');
INSERT INTO public."ManufactureCountry" VALUES (3, 'Германия');
INSERT INTO public."ManufactureCountry" VALUES (4, 'Япония');
INSERT INTO public."ManufactureCountry" VALUES (5, 'США');


--
-- TOC entry 5006 (class 0 OID 124367)
-- Dependencies: 232
-- Data for Name: Manufacturer; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Manufacturer" VALUES (1, 'Вендинг Технолоджи');
INSERT INTO public."Manufacturer" VALUES (2, 'Fuji Electric');
INSERT INTO public."Manufacturer" VALUES (3, 'Crane Merchandising Systems');
INSERT INTO public."Manufacturer" VALUES (4, 'Royal Vendors');
INSERT INTO public."Manufacturer" VALUES (5, 'Automatic Products');
INSERT INTO public."Manufacturer" VALUES (6, 'Saeco');
INSERT INTO public."Manufacturer" VALUES (7, 'Bianchi Industry');


--
-- TOC entry 5008 (class 0 OID 124374)
-- Dependencies: 234
-- Data for Name: PaymentType; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."PaymentType" VALUES (1, 'Наличные');
INSERT INTO public."PaymentType" VALUES (2, 'Банковская карта');
INSERT INTO public."PaymentType" VALUES (3, 'Мобильный платеж');
INSERT INTO public."PaymentType" VALUES (4, 'QR-код');


--
-- TOC entry 5010 (class 0 OID 124381)
-- Dependencies: 236
-- Data for Name: Problem; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Problem" VALUES (1, 'Зажевание товара');
INSERT INTO public."Problem" VALUES (2, 'Не принимает купюры');
INSERT INTO public."Problem" VALUES (3, 'Не выдает сдачу');
INSERT INTO public."Problem" VALUES (4, 'Не работает дисплей');
INSERT INTO public."Problem" VALUES (5, 'Перегрев аппарата');
INSERT INTO public."Problem" VALUES (6, 'Замыкание проводки');
INSERT INTO public."Problem" VALUES (7, 'Сбой программного обеспечения');
INSERT INTO public."Problem" VALUES (8, 'Повреждение корпуса');
INSERT INTO public."Problem" VALUES (9, 'Не охлаждает напитки');
INSERT INTO public."Problem" VALUES (10, 'Замок кассы не открывается');


--
-- TOC entry 5012 (class 0 OID 124388)
-- Dependencies: 238
-- Data for Name: Product; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Product" VALUES (1, 'Coca-Cola 0.5л', 'Газированный напиток', 80.00, 10);
INSERT INTO public."Product" VALUES (2, 'Pepsi 0.5л', 'Газированный напиток', 75.00, 10);
INSERT INTO public."Product" VALUES (3, 'Lays Картофельные чипсы', 'Чипсы со вкусом сметаны и лука', 120.00, 8);
INSERT INTO public."Product" VALUES (4, 'Snickers', 'Шоколадный батончик', 60.00, 15);
INSERT INTO public."Product" VALUES (5, 'Twix', 'Шоколадный батончик', 55.00, 15);
INSERT INTO public."Product" VALUES (6, 'Вода BonAqua 0.5л', 'Питьевая вода негазированная', 45.00, 12);
INSERT INTO public."Product" VALUES (7, 'Red Bull 0.25л', 'Энергетический напиток', 150.00, 6);
INSERT INTO public."Product" VALUES (8, 'KitKat', 'Шоколадный батончик', 50.00, 10);
INSERT INTO public."Product" VALUES (9, 'Orbit жевательная резинка', 'Жевательная резинка', 25.00, 20);
INSERT INTO public."Product" VALUES (10, 'Sokolad молочный шоколад', 'Плитка молочного шоколада', 90.00, 8);
INSERT INTO public."Product" VALUES (11, 'Nesquik 0.3л', 'Шоколадное молоко', 85.00, 8);
INSERT INTO public."Product" VALUES (12, '7UP 0.5л', 'Лимонно-лаймовый газированный напиток', 70.00, 10);


--
-- TOC entry 5014 (class 0 OID 124397)
-- Dependencies: 240
-- Data for Name: Role; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Role" VALUES (1, 'Администратор');
INSERT INTO public."Role" VALUES (2, 'Технический специалист');
INSERT INTO public."Role" VALUES (3, 'Оператор');
INSERT INTO public."Role" VALUES (4, 'Менеджер');


--
-- TOC entry 5016 (class 0 OID 124404)
-- Dependencies: 242
-- Data for Name: Sale; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Sale" VALUES (1, 1, 1, 1, '2024-01-10 08:30:00', 1);
INSERT INTO public."Sale" VALUES (2, 1, 4, 1, '2024-01-10 09:15:00', 2);
INSERT INTO public."Sale" VALUES (3, 2, 3, 1, '2024-01-10 10:20:00', 3);
INSERT INTO public."Sale" VALUES (4, 2, 7, 1, '2024-01-10 11:05:00', 2);
INSERT INTO public."Sale" VALUES (5, 3, 2, 1, '2024-01-10 12:30:00', 1);
INSERT INTO public."Sale" VALUES (6, 4, 1, 1, '2024-01-10 13:45:00', 4);
INSERT INTO public."Sale" VALUES (7, 4, 9, 2, '2024-01-10 14:20:00', 1);
INSERT INTO public."Sale" VALUES (8, 5, 4, 1, '2024-01-10 15:10:00', 2);
INSERT INTO public."Sale" VALUES (9, 1, 6, 1, '2024-01-10 16:25:00', 3);
INSERT INTO public."Sale" VALUES (10, 2, 5, 1, '2024-01-10 17:40:00', 1);
INSERT INTO public."Sale" VALUES (11, 1, 2, 1, '2024-02-05 09:30:00', 2);
INSERT INTO public."Sale" VALUES (12, 3, 4, 1, '2024-02-05 10:15:00', 1);
INSERT INTO public."Sale" VALUES (13, 4, 3, 1, '2024-02-05 11:20:00', 3);
INSERT INTO public."Sale" VALUES (14, 6, 1, 1, '2024-02-05 12:45:00', 2);
INSERT INTO public."Sale" VALUES (15, 7, 7, 1, '2024-02-05 14:10:00', 4);
INSERT INTO public."Sale" VALUES (16, 2, 9, 2, '2024-02-05 15:30:00', 1);
INSERT INTO public."Sale" VALUES (17, 1, 8, 1, '2024-03-12 08:45:00', 2);
INSERT INTO public."Sale" VALUES (18, 3, 6, 1, '2024-03-12 10:20:00', 1);
INSERT INTO public."Sale" VALUES (19, 5, 10, 1, '2024-03-12 11:35:00', 3);
INSERT INTO public."Sale" VALUES (20, 6, 11, 1, '2024-03-12 13:15:00', 2);
INSERT INTO public."Sale" VALUES (21, 7, 12, 1, '2024-03-12 14:50:00', 1);
INSERT INTO public."Sale" VALUES (22, 4, 5, 1, '2024-03-12 16:25:00', 4);
INSERT INTO public."Sale" VALUES (33, 7, 12, 1, '2025-11-07 19:51:04', 1);


--
-- TOC entry 5018 (class 0 OID 124411)
-- Dependencies: 244
-- Data for Name: User; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."User" VALUES (1, 'Иванов', 'Алексей', 'Петрович', 'ivanov@company.ru', '79161234567', 1, 'aivanov', '\x5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5');
INSERT INTO public."User" VALUES (2, 'Петрова', 'Мария', 'Сергеевна', 'petrova@company.ru', '79162345678', 2, 'mpetrova', '\x03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4');
INSERT INTO public."User" VALUES (3, 'Сидоров', 'Дмитрий', 'Игоревич', 'sidorov@company.ru', '79163456789', 2, 'dsidorov', '\x03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4');
INSERT INTO public."User" VALUES (4, 'Кузнецова', 'Елена', 'Владимировна', 'kuznetsova@company.ru', '79164567890', 3, 'ekuznetsova', '\xa665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3');
INSERT INTO public."User" VALUES (5, 'Васильев', 'Сергей', 'Александрович', 'vasilev@company.ru', '79165678901', 4, 'svasilev', '\x6b51d431df5d7f141cbececcf79edf3dd861c3b4069f0b11661a3eefacbba918');
INSERT INTO public."User" VALUES (6, 'Новиков', 'Андрей', 'Олегович', 'novikov@company.ru', '79166789012', 2, 'anovikov', '\x03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4');
INSERT INTO public."User" VALUES (10, 'Иванов', 'Алексей', 'Петрович', 'ivanov@company.ru', '79161234567', 1, 'iva', '\x240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9');


--
-- TOC entry 5020 (class 0 OID 124420)
-- Dependencies: 246
-- Data for Name: WorkDescription; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."WorkDescription" VALUES (1, 'Прочистка механизма выдачи');
INSERT INTO public."WorkDescription" VALUES (2, 'Замена купюроприемника');
INSERT INTO public."WorkDescription" VALUES (3, 'Ремонт системы охлаждения');
INSERT INTO public."WorkDescription" VALUES (4, 'Обновление программного обеспечения');
INSERT INTO public."WorkDescription" VALUES (5, 'Замена дисплея');
INSERT INTO public."WorkDescription" VALUES (6, 'Чистка внутренних компонентов');
INSERT INTO public."WorkDescription" VALUES (7, 'Калибровка датчиков');
INSERT INTO public."WorkDescription" VALUES (8, 'Замена блока питания');
INSERT INTO public."WorkDescription" VALUES (9, 'Заправка хладагента');
INSERT INTO public."WorkDescription" VALUES (10, 'Настройка платежной системы');
INSERT INTO public."WorkDescription" VALUES (11, 'Замена двигателя спирали');
INSERT INTO public."WorkDescription" VALUES (12, 'Профилактический осмотр');


--
-- TOC entry 5042 (class 0 OID 0)
-- Dependencies: 219
-- Name: MachineStatus_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."MachineStatus_Id_seq"', 6, true);


--
-- TOC entry 5043 (class 0 OID 0)
-- Dependencies: 217
-- Name: Machine_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Machine_Id_seq"', 8, true);


--
-- TOC entry 5044 (class 0 OID 0)
-- Dependencies: 221
-- Name: Machine_Product_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Machine_Product_Id_seq"', 36, true);


--
-- TOC entry 5045 (class 0 OID 0)
-- Dependencies: 223
-- Name: Maintenance_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Maintenance_Id_seq"', 9, true);


--
-- TOC entry 5046 (class 0 OID 0)
-- Dependencies: 225
-- Name: Maintenance_Problem_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Maintenance_Problem_Id_seq"', 9, true);


--
-- TOC entry 5047 (class 0 OID 0)
-- Dependencies: 227
-- Name: Maintenance_WorkDescribtion_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Maintenance_WorkDescribtion_Id_seq"', 12, true);


--
-- TOC entry 5048 (class 0 OID 0)
-- Dependencies: 229
-- Name: ManufactureCountry_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."ManufactureCountry_Id_seq"', 6, true);


--
-- TOC entry 5049 (class 0 OID 0)
-- Dependencies: 231
-- Name: Manufacturer_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Manufacturer_Id_seq"', 8, true);


--
-- TOC entry 5050 (class 0 OID 0)
-- Dependencies: 233
-- Name: PaymentType_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."PaymentType_Id_seq"', 5, true);


--
-- TOC entry 5051 (class 0 OID 0)
-- Dependencies: 235
-- Name: Problem_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Problem_Id_seq"', 11, true);


--
-- TOC entry 5052 (class 0 OID 0)
-- Dependencies: 237
-- Name: Product_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Product_Id_seq"', 13, true);


--
-- TOC entry 5053 (class 0 OID 0)
-- Dependencies: 239
-- Name: Role_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Role_Id_seq"', 4, true);


--
-- TOC entry 5054 (class 0 OID 0)
-- Dependencies: 241
-- Name: Sale_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Sale_Id_seq"', 33, true);


--
-- TOC entry 5055 (class 0 OID 0)
-- Dependencies: 243
-- Name: User_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."User_Id_seq"', 10, true);


--
-- TOC entry 5056 (class 0 OID 0)
-- Dependencies: 245
-- Name: WorkDescribtion_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."WorkDescribtion_Id_seq"', 13, true);


-- Completed on 2025-12-24 09:17:11

--
-- PostgreSQL database dump complete
--

