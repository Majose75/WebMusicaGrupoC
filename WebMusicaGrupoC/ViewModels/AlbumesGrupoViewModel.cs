﻿namespace WebMusicaGrupoC.ViewModels
{
    public class AlbumesGrupoViewModel
    {
        public string? GrupoId { get; set; }
        public List<ListaAlbumes>? AlbumGrupo { get; set; }
    }

    public class ListaAlbumes
    {
        public int AlbumesId { get;set; }
        public string? AlbumTitulo { get; set; }
    }
}
