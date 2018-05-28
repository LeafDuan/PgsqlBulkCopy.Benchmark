-- Table: public.crawlurl

-- DROP TABLE public.crawlurl;

CREATE TABLE public.crawlurl
(
  id integer NOT NULL DEFAULT nextval('crawlurl_id_seq'::regclass),
  code character(32),
  url text,
  sort integer,
  createtime timestamp without time zone,
  CONSTRAINT crawlurl_pkey PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.crawlurl
  OWNER TO postgres;

-- Index: public.idx_code

-- DROP INDEX public.idx_code;

CREATE INDEX idx_code
  ON public.crawlurl
  USING btree
  (code COLLATE pg_catalog."default");

