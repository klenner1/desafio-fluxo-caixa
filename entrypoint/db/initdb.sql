CREATE TABLE "Lancamentos" (
    "Id" uuid NOT NULL,
    "Descricao" text NOT NULL,
    "Valor" numeric NOT NULL,
    "Tipo" integer NOT NULL,
    CONSTRAINT "PK_Lancamentos" PRIMARY KEY ("Id")
);