namespace WebMusicaGrupoC.ViewModels
{
    public class AlbumesGrupoViewModel
    {
        public string Album { get; set; }
        public List<ListaGrupos> GrupoAlbum { get; set; }
    }

    public class ListaGrupos
    {
        public int GruposId { get;set; }
        public string GruposNombre { get; set; }
    }
}
