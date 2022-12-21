using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Extensions;
using TFG2022Server.Models.ReportModels;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class VentasPedidoReportService : IVentasPedidoReportService
    {
        private readonly TFG2022Context tfg2022Context;
        public VentasPedidoReportService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public async Task<List<GroupedFieldCantidadModel>> GetCantidadPorFamiliaProducto()
        {
            try
            {
                var reportData = await (from v in this.tfg2022Context.VentasPedidoReportes
                                        group v by v.FamiliaProductoNombre into GroupedData
                                        orderby GroupedData.Key
                                        select new GroupedFieldCantidadModel
                                        {
                                            GroupedFieldCantidadKey = GroupedData.Key,
                                            Cantidad = GroupedData.Sum(lp => lp.LineaPedidoCantidad)
                                        }).ToListAsync();
                return reportData;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<GroupedFieldCantidadModel>> GetProductosVendidosPorTiempoData(DateTime startDate, DateTime endDate)
        {
            try
            {
                var reportData = await (from v in this.tfg2022Context.VentasPedidoReportes
                                        where v.FechaPedido >= startDate && v.FechaPedido <= endDate
                                        group v by v.NombreProducto into GroupedData
                                        orderby GroupedData.Key
                                        select new GroupedFieldCantidadModel
                                        {
                                            GroupedFieldCantidadKey = GroupedData.Key.Substring(0, 20),
                                            Cantidad = GroupedData.Sum(lp => lp.LineaPedidoCantidad)
                                        }).ToListAsync();
                return reportData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GroupedFieldCantidadModel>> GetFamiliaProductosVendidosPorTiempoData(DateTime startDate, DateTime endDate)
        {
            try
            {
                var reportData = await (from v in this.tfg2022Context.VentasPedidoReportes
                                        where v.FechaPedido >= startDate && v.FechaPedido <= endDate
                                        group v by v.FamiliaProductoNombre into GroupedData
                                        orderby GroupedData.Key
                                        select new GroupedFieldCantidadModel
                                        {
                                            GroupedFieldCantidadKey = GroupedData.Key.Substring(0, 20),
                                            Cantidad = GroupedData.Sum(lp => lp.LineaPedidoCantidad)
                                        }).ToListAsync();
                return reportData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GroupedFieldTogetherModel>> GetProductosVendidosJuntosPorTiempoData(DateTime startDate, DateTime endDate)
        {
            try
            {
                List<GroupedFieldTogetherModel> result = new List<GroupedFieldTogetherModel>();

                var reportData = await (from t1 in this.tfg2022Context.VentasPedidoReportes
                                        join t2 in this.tfg2022Context.VentasPedidoReportes on t1.PedidoId equals t2.PedidoId
                                        select new { t1 = t1, t2 = t2 }
                 ).Where(x => x.t1.ProductoId < x.t2.ProductoId)
                  .GroupBy(x => new { t1Id = x.t1.ProductoId, t2Id = x.t2.ProductoId })
                  .Select(x => new { GroupedFieldProduct1Key = x.First().t1.NombreProducto, GroupedFieldProduct2Key = x.First().t2.NombreProducto, Cantidad = x.Select(y => y.t1.PedidoId).Distinct().Count(), Fecha = x.First().t1.FechaPedido })
                  .OrderByDescending(x => x.Cantidad).ToListAsync();

                foreach (var item in reportData)
                {
                    if (item.Fecha >= startDate && item.Fecha <= endDate)
                        result.Add(new GroupedFieldTogetherModel(item.GroupedFieldProduct1Key, item.GroupedFieldProduct2Key, item.Cantidad));
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GroupedFieldDateModel>> GetPedidosPorTiempoData(DateTime startDate, DateTime endDate)
        {
            try
            {
                var reportData = await (from v in this.tfg2022Context.VentasPedidoReportes
                                        group v by new { v.FechaPedido, v.PedidoId } into g
                                        where g.Key.FechaPedido >= startDate && g.Key.FechaPedido <= endDate
                                        select new { g.Key.FechaPedido, g.Key.PedidoId })
                    .GroupBy(x => new { year = x.FechaPedido.Year, month = x.FechaPedido.Month, day = x.FechaPedido.Day })
                    .Select(x => new GroupedFieldDateModel { GroupedFieldDateKey = new DateTime(x.Key.year, x.Key.month, x.Key.day), Cantidad = x.Select(y => y.PedidoId).Distinct().Count() }).ToListAsync();


                return reportData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GroupedFieldCantidadModel>> GetProductosEnCarritos()
        {
            try
            {
                var lineasCarrito = await this.tfg2022Context.LineaCarritos.Convert();
                var productos = await this.tfg2022Context.Productos.Convert(tfg2022Context);

                List<string> repeatList = new List<string>();

                foreach (var item in lineasCarrito)
                {
                    repeatList.Add((await tfg2022Context.Productos.FindAsync(item.ProductoLineaCarrito)).Nombre);
                }
                List<GroupedFieldCantidadModel> sol = new List<GroupedFieldCantidadModel>();
                foreach (var item in repeatList.GroupBy(x => x).Select(y => new GroupedFieldCantidadModel { GroupedFieldCantidadKey = y.Key, Cantidad = y.Count() }).ToList())
                {
                    if (item.GroupedFieldCantidadKey.Length >= 20) sol.Add(new GroupedFieldCantidadModel { GroupedFieldCantidadKey = item.GroupedFieldCantidadKey.Substring(0, 20), Cantidad = item.Cantidad });
                    else sol.Add(new GroupedFieldCantidadModel { GroupedFieldCantidadKey = item.GroupedFieldCantidadKey, Cantidad = item.Cantidad });
                }

                return sol;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<MunicipioDetailsModel>> GetUsuariosPuntosMapa()
        {
            try
            {
                List<MunicipioDetailsModel> pueblos = new List<MunicipioDetailsModel> {
                    new MunicipioDetailsModel { CodigoPostal="29530", Name="Alameda", Latitude=37.2320039792079, Longitude=-4.6651912841, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29711", Name="Alcaucín", Latitude=36.9043844808312, Longitude=-4.1301718570, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29194", Name="Alfarnate", Latitude=36.9888854025488, Longitude=-4.2542294261, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29194", Name="Alfarnatejo", Latitude=36.9660173186309, Longitude=-4.2635318005, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29750", Name="Algarrobo", Latitude=36.7607314316932, Longitude=-4.0450602259, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29491", Name="Algatocín", Latitude=36.575095809672, Longitude=-5.28317316049, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29130", Name="Alhaurín de la Torre", Latitude=36.6718834202746, Longitude=-4.5679697738, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29120", Name="Alhaurín el Grande", Latitude=36.6423851897924, Longitude=-4.6907868796, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29718", Name="Almáchar", Latitude=36.8319342274112, Longitude=-4.2185084383, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29330", Name="Almargen", Latitude=37.0237959948334, Longitude=-5.0144108073, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29150", Name="Almogía", Latitude=36.8362936368303, Longitude=-4.5332563706, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29500", Name="Álora", Latitude=36.8366757998857, Longitude=-4.7037755713, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29567", Name="Alozaina", Latitude=36.7383195331784, Longitude=-4.8641290185, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29460", Name="Alpandeire", Latitude=36.6464132247266, Longitude=-5.2232595433, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29200", Name="Antequera", Latitude=37.0548852510426, Longitude=-4.5928281775, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29753", Name="Árchez", Latitude=36.8270092848641, Longitude=-3.9951508939, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29300", Name="Archidona", Latitude=37.099453190792, Longitude=-4.38851560153, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29550", Name="Ardales", Latitude=36.8898743273109, Longitude=-4.8368476731, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29753", Name="Arenas", Latitude=36.8193480941124, Longitude=-4.0708139707, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29350", Name="Arriate", Latitude=36.7973541352046, Longitude=-5.1369926552, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29494", Name="Atajate", Latitude=36.6465680796357, Longitude=-5.2420650448, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29493", Name="Benadalid", Latitude=36.6149246044478, Longitude=-5.2674450215, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29679", Name="Benahavís", Latitude=36.5635953710525, Longitude=-5.0422173841, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29491", Name="Benalauría", Latitude=36.5844678519834, Longitude=-5.3022639993, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29639", Name="Benalmádena", Latitude=36.5897530862559, Longitude=-4.5421327809, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29718", Name="Benamargosa", Latitude=36.8451007520181, Longitude=-4.1957014391, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29719", Name="Benamocarra", Latitude=36.790217000208, Longitude=-4.16424235057, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29370", Name="Benaoján", Latitude=36.7086665317906, Longitude=-5.2565809268, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29490", Name="Benarrabá", Latitude=36.5490854537034, Longitude=-5.3047204372, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29718", Name="El Borge", Latitude=36.8249976116206, Longitude=-4.2566211362, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29420", Name="El Burgo", Latitude=36.8039356822714, Longitude=-4.9369838534, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29320", Name="Campillos", Latitude=37.0029527901888, Longitude=-4.8540957096, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29716", Name="Canillas de Aceituno", Latitude=36.9570089302833, Longitude=-5.0433218416, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29755", Name="Canillas de Albaida", Latitude=36.8641103763851, Longitude=-4.1008872008, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29395", Name="Cañete la Real", Latitude=36.8503886398044, Longitude=-3.9793681255, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29551", Name="Carratraca", Latitude=36.8468003472164, Longitude=-4.8217895045, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29452", Name="Cartajima", Latitude=36.6527142829085, Longitude=-5.1511289397, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29570", Name="Cártama", Latitude=36.7439962299026, Longitude=-4.6679571363, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29160", Name="Casabermeja", Latitude=36.866055199273, Longitude=-4.42680589618, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29566", Name="Casarabonela", Latitude=36.8017823726424, Longitude=-4.8290976842, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29690", Name="Casares", Latitude=36.4308833487603, Longitude=-5.2692204225, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29100", Name="Coín", Latitude=36.6744990801695, Longitude=-4.7661059513, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29170", Name="Colmenar", Latitude=36.9136040990183, Longitude=-4.3211654615, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29195", Name="Comares", Latitude=36.8555548388787, Longitude=-4.2619849681, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29754", Name="Cómpeta", Latitude=36.8333080748342, Longitude=-3.9268918954, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29380", Name="Cortes de la Frontera", Latitude=36.5694832187731, Longitude=-5.4315437812, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29220", Name="Cuevas Bajas", Latitude=37.2300103047166, Longitude=-4.4793112421, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29210", Name="Cuevas de San Marcos", Latitude=37.2607230038989, Longitude=-4.4269460174, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29470", Name="Cuevas del Becerro", Latitude=36.8772969806384, Longitude=-5.0386300213, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29718", Name="Cútar", Latitude=36.8654531947358, Longitude=-4.2242237674, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29680", Name="Estepona", Latitude=36.4454332345383, Longitude=-5.1273913259, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29461", Name="Faraján", Latitude=36.6124468497466, Longitude=-5.1965715470, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29788", Name="Frigiliana", Latitude=36.8102691480879, Longitude=-3.8868405199, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29640", Name="Fuengirola", Latitude=36.5514938222835, Longitude=-4.6120551668, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29520", Name="Fuente de Piedra", Latitude=37.1259042225459, Longitude=-4.7510660088, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29480", Name="Gaucín", Latitude=36.5131293883127, Longitude=-5.3436405286, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29492", Name="Genalguacil", Latitude=36.5263133183844, Longitude=-5.2322431797, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29108", Name="Guaro", Latitude=36.6740548239202, Longitude=-4.8373058526, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29531", Name="Humilladero", Latitude=37.1332579667016, Longitude=-4.6871220062, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29440", Name="Igualeja", Latitude=36.624797493798, Longitude=-5.09594994384, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29611", Name="Istán", Latitude=36.6131595962698, Longitude=-4.9784219621, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29792", Name="Iznate", Latitude=36.7848126009216, Longitude=-4.1879015234, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29392", Name="Jimera de Líbar", Latitude=36.6520122871962, Longitude=-5.2806885364, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29492", Name="Jubrique", Latitude=36.5684910498035, Longitude=-5.2186695579, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29462", Name="Júzcar", Latitude=36.5945838393094, Longitude=-5.1560547003, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29791", Name="Macharaviaya", Latitude=36.7687108440945, Longitude=-4.2215769506, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29001", Name="Malaga", Latitude=36.7585406465564, Longitude=-4.3971722687, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29691", Name="Manilva", Latitude=36.3487809196413, Longitude=-5.2521294264, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29601", Name="Marbella", Latitude=36.4997747284402, Longitude=-4.8842120341, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29650", Name="Mijas", Latitude=36.5350704417103, Longitude=-4.6735474359, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29738", Name="Moclinejo", Latitude=36.7672828325886, Longitude=-4.2608153335, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29532", Name="Mollina", Latitude=37.1378450537172, Longitude=-4.6412797700, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29110", Name="Monda", Latitude=36.6250181467817, Longitude=-4.8475735297, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29360", Name="Montejaque", Latitude=36.7284111729776, Longitude=-5.2851863065, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29780", Name="Nerja", Latitude=36.7645977001364, Longitude=-3.8399798239, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29610", Name="Ojén", Latitude=36.5612259664336, Longitude=-4.8068808982, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29451", Name="Parauta", Latitude=36.6656851910272, Longitude=-5.0820901949, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29710", Name="Periana", Latitude=36.9344524266528, Longitude=-4.1868842456, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29560", Name="Pizarra", Latitude=36.7692072478151, Longitude=-4.7013889479, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29450", Name="Pujerra", Latitude=36.5893668582753, Longitude=-5.1375092865, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29730", Name="Rincón de la Victoria", Latitude=36.7245684036249, Longitude=-4.2710127862, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29180", Name="Riogordo", Latitude=36.9212999990663, Longitude=-4.2859650848, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29400", Name="Ronda", Latitude=36.80214801548, Longitude=-5.139344170769, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29714", Name="Salares", Latitude=36.8572473961815, Longitude=-4.0155246251, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29752", Name="Sayalonga", Latitude=36.7978697805915, Longitude=-4.0054789806, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29715", Name="Sedella", Latitude=36.8499612219442, Longitude=-4.0554133123, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29328", Name="Sierra de Yeguas", Latitude=37.1616919343317, Longitude=-4.8654806491, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29327", Name="Teba", Latitude=36.9724839411986, Longitude=-4.9059021505, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29109", Name="Tolox", Latitude=36.6754095576812, Longitude=-4.9374076740, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29620", Name="Torremolinos", Latitude=36.6238677769039, Longitude=-4.5045827340, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29770", Name="Torrox", Latitude=36.7579665320365, Longitude=-3.9585537837, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29197", Name="Totalán", Latitude=36.7601646256474, Longitude=-4.2928930199, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29240", Name="Valle de Abdalajís", Latitude=36.9387373481701, Longitude=-4.6704258609, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29700", Name="Vélez Málaga", Latitude=36.7607100916559, Longitude=-4.1232077759, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29310", Name="Villanueva de Algaidas", Latitude=37.1810727976616, Longitude=-4.4095310030, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29315", Name="Villanueva de Tapia", Latitude=37.1856076877034, Longitude=-4.3424655677, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29312", Name="Villanueva del Rosario", Latitude=37.0099166323718, Longitude=-4.3779554576, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29313", Name="Villanueva del Trabuco", Latitude=37.0478555633015, Longitude=-4.3221103807, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29712", Name="Viñuela", Latitude=36.8615820335835, Longitude=-4.1453074070, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29410", Name="Yunquera", Latitude=36.7354267885242, Longitude=-4.9271544535, Usuarios=0, Producto=""}
                };
                List<MunicipioDetailsModel> result = new List<MunicipioDetailsModel>();

                var usuarios = await (from v in this.tfg2022Context.Usuarios
                                      group v by v.CodigoPostal into g
                                      select new { g.Key, usuarios = g.Count() }).ToListAsync();
                foreach (var pueblo in pueblos)
                {
                    foreach (var usuario in usuarios)
                    {
                        if (pueblo.CodigoPostal == usuario.Key) 
                        {
                            result.Add( new MunicipioDetailsModel() { CodigoPostal = pueblo.CodigoPostal, Latitude = pueblo.Latitude, Longitude = pueblo.Longitude, Name = pueblo.Name, Usuarios = usuario.usuarios });
                        }
                    }
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private bool pueblosNoContienePueblo(List<MunicipioDetailsModel> pueblos, string CodPost)
        {
            foreach (var item in pueblos)
            {
                if (item.CodigoPostal == CodPost) return false;
            }
            return true;
        }
        public async Task<List<MunicipioDetailsModel>> GetProductosPuntosMapa()
        {
            try
            {
                List<MunicipioDetailsModel> pueblos = new List<MunicipioDetailsModel> {
                    new MunicipioDetailsModel { CodigoPostal="29530", Name="Alameda", Latitude=37.2320039792079, Longitude=-4.6651912841, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29711", Name="Alcaucín", Latitude=36.9043844808312, Longitude=-4.1301718570, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29194", Name="Alfarnate", Latitude=36.9888854025488, Longitude=-4.2542294261, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29194", Name="Alfarnatejo", Latitude=36.9660173186309, Longitude=-4.2635318005, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29750", Name="Algarrobo", Latitude=36.7607314316932, Longitude=-4.0450602259, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29491", Name="Algatocín", Latitude=36.575095809672, Longitude=-5.28317316049, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29130", Name="Alhaurín de la Torre", Latitude=36.6718834202746, Longitude=-4.5679697738, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29120", Name="Alhaurín el Grande", Latitude=36.6423851897924, Longitude=-4.6907868796, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29718", Name="Almáchar", Latitude=36.8319342274112, Longitude=-4.2185084383, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29330", Name="Almargen", Latitude=37.0237959948334, Longitude=-5.0144108073, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29150", Name="Almogía", Latitude=36.8362936368303, Longitude=-4.5332563706, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29500", Name="Álora", Latitude=36.8366757998857, Longitude=-4.7037755713, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29567", Name="Alozaina", Latitude=36.7383195331784, Longitude=-4.8641290185, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29460", Name="Alpandeire", Latitude=36.6464132247266, Longitude=-5.2232595433, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29200", Name="Antequera", Latitude=37.0548852510426, Longitude=-4.5928281775, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29753", Name="Árchez", Latitude=36.8270092848641, Longitude=-3.9951508939, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29300", Name="Archidona", Latitude=37.099453190792, Longitude=-4.38851560153, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29550", Name="Ardales", Latitude=36.8898743273109, Longitude=-4.8368476731, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29753", Name="Arenas", Latitude=36.8193480941124, Longitude=-4.0708139707, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29350", Name="Arriate", Latitude=36.7973541352046, Longitude=-5.1369926552, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29494", Name="Atajate", Latitude=36.6465680796357, Longitude=-5.2420650448, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29493", Name="Benadalid", Latitude=36.6149246044478, Longitude=-5.2674450215, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29679", Name="Benahavís", Latitude=36.5635953710525, Longitude=-5.0422173841, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29491", Name="Benalauría", Latitude=36.5844678519834, Longitude=-5.3022639993, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29639", Name="Benalmádena", Latitude=36.5897530862559, Longitude=-4.5421327809, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29718", Name="Benamargosa", Latitude=36.8451007520181, Longitude=-4.1957014391, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29719", Name="Benamocarra", Latitude=36.790217000208, Longitude=-4.16424235057, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29370", Name="Benaoján", Latitude=36.7086665317906, Longitude=-5.2565809268, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29490", Name="Benarrabá", Latitude=36.5490854537034, Longitude=-5.3047204372, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29718", Name="El Borge", Latitude=36.8249976116206, Longitude=-4.2566211362, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29420", Name="El Burgo", Latitude=36.8039356822714, Longitude=-4.9369838534, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29320", Name="Campillos", Latitude=37.0029527901888, Longitude=-4.8540957096, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29716", Name="Canillas de Aceituno", Latitude=36.9570089302833, Longitude=-5.0433218416, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29755", Name="Canillas de Albaida", Latitude=36.8641103763851, Longitude=-4.1008872008, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29395", Name="Cañete la Real", Latitude=36.8503886398044, Longitude=-3.9793681255, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29551", Name="Carratraca", Latitude=36.8468003472164, Longitude=-4.8217895045, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29452", Name="Cartajima", Latitude=36.6527142829085, Longitude=-5.1511289397, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29570", Name="Cártama", Latitude=36.7439962299026, Longitude=-4.6679571363, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29160", Name="Casabermeja", Latitude=36.866055199273, Longitude=-4.42680589618, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29566", Name="Casarabonela", Latitude=36.8017823726424, Longitude=-4.8290976842, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29690", Name="Casares", Latitude=36.4308833487603, Longitude=-5.2692204225, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29100", Name="Coín", Latitude=36.6744990801695, Longitude=-4.7661059513, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29170", Name="Colmenar", Latitude=36.9136040990183, Longitude=-4.3211654615, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29195", Name="Comares", Latitude=36.8555548388787, Longitude=-4.2619849681, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29754", Name="Cómpeta", Latitude=36.8333080748342, Longitude=-3.9268918954, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29380", Name="Cortes de la Frontera", Latitude=36.5694832187731, Longitude=-5.4315437812, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29220", Name="Cuevas Bajas", Latitude=37.2300103047166, Longitude=-4.4793112421, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29210", Name="Cuevas de San Marcos", Latitude=37.2607230038989, Longitude=-4.4269460174, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29470", Name="Cuevas del Becerro", Latitude=36.8772969806384, Longitude=-5.0386300213, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29718", Name="Cútar", Latitude=36.8654531947358, Longitude=-4.2242237674, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29680", Name="Estepona", Latitude=36.4454332345383, Longitude=-5.1273913259, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29461", Name="Faraján", Latitude=36.6124468497466, Longitude=-5.1965715470, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29788", Name="Frigiliana", Latitude=36.8102691480879, Longitude=-3.8868405199, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29640", Name="Fuengirola", Latitude=36.5514938222835, Longitude=-4.6120551668, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29520", Name="Fuente de Piedra", Latitude=37.1259042225459, Longitude=-4.7510660088, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29480", Name="Gaucín", Latitude=36.5131293883127, Longitude=-5.3436405286, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29492", Name="Genalguacil", Latitude=36.5263133183844, Longitude=-5.2322431797, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29108", Name="Guaro", Latitude=36.6740548239202, Longitude=-4.8373058526, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29531", Name="Humilladero", Latitude=37.1332579667016, Longitude=-4.6871220062, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29440", Name="Igualeja", Latitude=36.624797493798, Longitude=-5.09594994384, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29611", Name="Istán", Latitude=36.6131595962698, Longitude=-4.9784219621, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29792", Name="Iznate", Latitude=36.7848126009216, Longitude=-4.1879015234, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29392", Name="Jimera de Líbar", Latitude=36.6520122871962, Longitude=-5.2806885364, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29492", Name="Jubrique", Latitude=36.5684910498035, Longitude=-5.2186695579, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29462", Name="Júzcar", Latitude=36.5945838393094, Longitude=-5.1560547003, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29791", Name="Macharaviaya", Latitude=36.7687108440945, Longitude=-4.2215769506, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29001", Name="Malaga", Latitude=36.7585406465564, Longitude=-4.3971722687, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29691", Name="Manilva", Latitude=36.3487809196413, Longitude=-5.2521294264, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29601", Name="Marbella", Latitude=36.4997747284402, Longitude=-4.8842120341, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29650", Name="Mijas", Latitude=36.5350704417103, Longitude=-4.6735474359, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29738", Name="Moclinejo", Latitude=36.7672828325886, Longitude=-4.2608153335, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29532", Name="Mollina", Latitude=37.1378450537172, Longitude=-4.6412797700, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29110", Name="Monda", Latitude=36.6250181467817, Longitude=-4.8475735297, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29360", Name="Montejaque", Latitude=36.7284111729776, Longitude=-5.2851863065, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29780", Name="Nerja", Latitude=36.7645977001364, Longitude=-3.8399798239, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29610", Name="Ojén", Latitude=36.5612259664336, Longitude=-4.8068808982, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29451", Name="Parauta", Latitude=36.6656851910272, Longitude=-5.0820901949, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29710", Name="Periana", Latitude=36.9344524266528, Longitude=-4.1868842456, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29560", Name="Pizarra", Latitude=36.7692072478151, Longitude=-4.7013889479, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29450", Name="Pujerra", Latitude=36.5893668582753, Longitude=-5.1375092865, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29730", Name="Rincón de la Victoria", Latitude=36.7245684036249, Longitude=-4.2710127862, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29180", Name="Riogordo", Latitude=36.9212999990663, Longitude=-4.2859650848, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29400", Name="Ronda", Latitude=36.80214801548, Longitude=-5.139344170769, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29714", Name="Salares", Latitude=36.8572473961815, Longitude=-4.0155246251, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29752", Name="Sayalonga", Latitude=36.7978697805915, Longitude=-4.0054789806, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29715", Name="Sedella", Latitude=36.8499612219442, Longitude=-4.0554133123, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29328", Name="Sierra de Yeguas", Latitude=37.1616919343317, Longitude=-4.8654806491, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29327", Name="Teba", Latitude=36.9724839411986, Longitude=-4.9059021505, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29109", Name="Tolox", Latitude=36.6754095576812, Longitude=-4.9374076740, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29620", Name="Torremolinos", Latitude=36.6238677769039, Longitude=-4.5045827340, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29770", Name="Torrox", Latitude=36.7579665320365, Longitude=-3.9585537837, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29197", Name="Totalán", Latitude=36.7601646256474, Longitude=-4.2928930199, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29240", Name="Valle de Abdalajís", Latitude=36.9387373481701, Longitude=-4.6704258609, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29700", Name="Vélez Málaga", Latitude=36.7607100916559, Longitude=-4.1232077759, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29310", Name="Villanueva de Algaidas", Latitude=37.1810727976616, Longitude=-4.4095310030, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29315", Name="Villanueva de Tapia", Latitude=37.1856076877034, Longitude=-4.3424655677, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29312", Name="Villanueva del Rosario", Latitude=37.0099166323718, Longitude=-4.3779554576, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29313", Name="Villanueva del Trabuco", Latitude=37.0478555633015, Longitude=-4.3221103807, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29712", Name="Viñuela", Latitude=36.8615820335835, Longitude=-4.1453074070, Usuarios=0, Producto=""},
                    new MunicipioDetailsModel { CodigoPostal="29410", Name="Yunquera", Latitude=36.7354267885242, Longitude=-4.9271544535, Usuarios=0, Producto=""}
                };
                List<MunicipioDetailsModel> result = new List<MunicipioDetailsModel>();

                var productos = await ((from v in this.tfg2022Context.VentasPedidoReportes
                                        group v by new { v.UsuarioCodigoPostal, v.NombreProducto } into g
                                        orderby g.Count() descending
                                        select new { g.Key.UsuarioCodigoPostal, g.Key.NombreProducto, usuarios = g.Count() })
                                        .OrderByDescending(g => g.usuarios)).ToListAsync();

                foreach (var pueblo in pueblos)
                {
                    foreach (var producto in productos)
                    {
                        if (pueblo.CodigoPostal == producto.UsuarioCodigoPostal && pueblosNoContienePueblo(result, pueblo.CodigoPostal))
                        {
                            result.Add(new MunicipioDetailsModel() { CodigoPostal = pueblo.CodigoPostal, Latitude = pueblo.Latitude, Longitude = pueblo.Longitude, Name = pueblo.Name, Producto = producto.NombreProducto, Usuarios= producto.usuarios });
                        }
                    }
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GroupedFieldCantidadModel>> GetEstadoPedidos()
        {
            try
            {
                var reportData = await(from v in this.tfg2022Context.Pedidos
                                       group v by v.EstadoPedido into g
                                       select new GroupedFieldCantidadModel { GroupedFieldCantidadKey =g.Key, Cantidad = g.Count() }).ToListAsync();
                return reportData;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}

