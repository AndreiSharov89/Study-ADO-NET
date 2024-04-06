DROP DATABASE IF EXISTS "zip_codes";
CREATE DATABASE "zip_codes"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Russian_Russia.1251'
    LC_CTYPE = 'Russian_Russia.1251'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;
--table 1
DROP TABLE IF EXISTS public.countries;
CREATE TABLE IF NOT EXISTS public.countries
(
    country_id smallint NOT NULL,
    country_code character varying COLLATE pg_catalog."default" NOT NULL,
    country_name character varying COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT countries_pk PRIMARY KEY (country_id)
)
TABLESPACE pg_default;
ALTER TABLE IF EXISTS public.countries
    OWNER to postgres;
UPDATE public.countries
	SET country_name='Réunion'
	WHERE country_code='RE';

--table 2
DROP TABLE IF EXISTS public.zipaddresses;
CREATE TABLE IF NOT EXISTS public.zipaddresses
(
    zip_id integer NOT NULL,
    country_id smallint NOT NULL,
    zip_code character varying COLLATE pg_catalog."default" NOT NULL,
    city character varying COLLATE pg_catalog."default",
    state character varying COLLATE pg_catalog."default",
    county character varying COLLATE pg_catalog."default",
    community character varying COLLATE pg_catalog."default",
    CONSTRAINT zipaddresses_pkey PRIMARY KEY (country_id, zip_code),
    CONSTRAINT zipaddresses_country_id_fkey FOREIGN KEY (country_id)
        REFERENCES public.countries (country_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
TABLESPACE pg_default;
ALTER TABLE IF EXISTS public.zipaddresses
    OWNER to postgres;

--table 3
DROP TABLE IF EXISTS public.zipcoords;
CREATE TABLE IF NOT EXISTS public.zipcoords
(
    zip_id integer NOT NULL,
    country_id smallint NOT NULL,
    zip_code character varying COLLATE pg_catalog."default" NOT NULL,
    lat double precision NOT NULL,
    lon double precision NOT NULL,
    accuracy smallint,
    CONSTRAINT zipcoords_pkey PRIMARY KEY (country_id, zip_code),
    CONSTRAINT zipcoords_country_id_fkey FOREIGN KEY (country_id)
        REFERENCES public.countries (country_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
TABLESPACE pg_default;
ALTER TABLE IF EXISTS public.zipcoords
    OWNER to postgres;

--import data
copy public.countries (country_id, country_code, country_name) FROM 'C:/try/COUNTRIES_out.CSV' DELIMITER ',' CSV ENCODING 'WIN1251' QUOTE '"' ESCAPE '''';
copy public.zipaddresses (zip_id, country_id, zip_code, city, state, county, community) FROM 'C:/try/ZIPADDRESSES2.CSV' DELIMITER ',' CSV ENCODING 'UTF8' QUOTE '"';
copy public.zipcoords (zip_id, country_id, zip_code, lat, lon, accuracy) FROM 'C:/try/ZIPCOORDS2.CSV' DELIMITER ',' CSV HEADER ENCODING 'UTF8' QUOTE '"';

--func1
CREATE OR REPLACE FUNCTION public.get_address_f(
	c_code character varying,
	z_code character varying)
    RETURNS TABLE(ZIP character varying, Country character varying, City character varying, State character varying, County character varying, Community character varying) 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select z.zip_code, c.country_name, z.city, z.state, z.county, z.community
	FROM public.zipaddresses as z join public.countries as c on z.country_id = c.country_id where c.country_code = c_code and z.zip_code = z_code;
$BODY$;

ALTER FUNCTION public.get_address_f(character varying, character varying)
    OWNER TO postgres;

select get_address_f('RU', '150000');
select * from get_address_f('RU', '150000');

--func2
CREATE OR REPLACE FUNCTION public.get_coords_f(
	c_code character varying,
	z_code character varying)
    RETURNS TABLE(Latitude double precision, Longtitude double precision) 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select z.lat as Latitude, z.lon as Longtitude
	FROM public.zipcoords as z join public.countries as c on z.country_id = c.country_id where c.country_code = c_code and z.zip_code = z_code;
$BODY$;

ALTER FUNCTION public.get_address_f(character varying, character varying)
    OWNER TO postgres;

select get_coords_f('RU', '150000');
select * from get_coords_f('RU', '150000');
select latitude from get_coords_f('RU', '150000');
select longtitude from get_coords_f('RU', '150000');


--func 3
CREATE OR REPLACE FUNCTION public.get_zips_f(
	c_code character varying)
    RETURNS TABLE(ZIP character varying, City character varying, State character varying) 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 100000

AS $BODY$
select z.zip_code, z.city, z.state
	FROM public.zipaddresses as z join public.countries as c on z.country_id = c.country_id where c.country_code = c_code;
$BODY$;

ALTER FUNCTION public.get_zips_f(character varying)
    OWNER TO postgres;
select * from get_zips_f('US');

--rights
CREATE ROLE zip_r;
GRANT CONNECT ON DATABASE zip_codes TO zip_r;
GRANT USAGE ON SCHEMA public TO zip_r;
GRANT SELECT ON ALL TABLES IN SCHEMA public TO zip_r;
GRANT EXECUTE ON ALL FUNCTIONS IN SCHEMA public TO zip_r;
CREATE USER zip_user WITH PASSWORD 'user';
GRANT zip_r TO zip_user;

CREATE ROLE zip_rw;
GRANT CONNECT ON DATABASE zip_codes TO zip_rw;
GRANT USAGE ON SCHEMA public TO zip_rw;
GRANT ALL PRIVILEGES ON DATABASE zip_codes TO zip_rw;
GRANT ALL ON ALL TABLES IN SCHEMA public TO zip_rw;
GRANT ALL ON ALL FUNCTIONS IN SCHEMA public TO zip_rw;
CREATE USER zip_admin WITH PASSWORD 'admin';
GRANT zip_rw TO zip_admin;



