using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jamie.Npcs {
	public static class ClientLevelMap {
		public static int getIdFromString(string name){
			foreach (var map in mapToId) {
				if (map.Key == name) return map.Value;
			}
			return -1;
		}

		public static string getStringFromId(int id) {
			foreach (var map in mapToId) {
				if (map.Value == id) return map.Key;
			}
			return null;
		}

		public static bool isMap(string name) {
			int mapId = 0;
			Int32.TryParse(name, out mapId);

			foreach (int map in mapToId.Values) {
				if (map == mapId) return true;
			}
			return false;
		}

		public static Dictionary<string, int> mapToId = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase)
        {
			{"lc1", 110010000 }, 
			{"lc2", 110020000 }, 
			{"dc1", 120010000 }, 
			{"dc2", 120020000 }, 
			{"arena_l_lobby", 110070000 }, 
			{"arena_d_lobby", 120080000 }, 
			{"lf1", 210010000 }, 
			{"lf2", 210020000 }, 
			{"lf1a", 210030000 }, 
			{"lf3", 210040000 }, 
			{"lf4", 210050000 }, 
			{"lf2a", 210060000 }, 
			{"df1", 220010000 }, 
			{"df2", 220020000 }, 
			{"df1a", 220030000 }, 
			{"df3", 220040000 }, 
			{"df2a", 220050000 }, 
			{"df4", 220070000 }, 
			{"idabpro", 300010000 }, 
			{"idtest_dungeon", 300020000 }, 
			{"idab1_minicastle", 300030000 }, 
			{"idlf1", 300040000 }, 
			{"idabre_up_asteria", 300050000 }, 
			{"idabre_low_divine", 300060000 }, 
			{"idabre_up_rhoo", 300070000 }, 
			{"idabre_low_wciel", 300080000 }, 
			{"idabre_low_eciel", 300090000 }, 
			{"idshulackship", 300100000 }, 
			{"idab1_dreadgion", 300110000 }, 
			{"idabre_up3_dkisas", 300120000 }, 
			{"idabre_up3_lamiren", 300130000 }, 
			{"idabre_up3_crotan", 300140000 }, 
			{"IDTemple_Up", 300150000 }, 
			{"IDTemple_Low", 300160000 }, 
			{"idcatacombs", 300170000 }, 
			{"idelim", 300190000 }, 
			{"idnovice", 300200000 }, 
			{"iddreadgion_02", 300210000 }, 
			{"idabre_core", 300220000 }, 
			{"idcromede", 300230000 }, 
			{"idstation", 300240000 }, 
			{"idf4re_drana", 300250000 }, 
			{"idelemental_1", 300260000 }, 
			{"idelemental_2", 300270000 }, 
			{"idyun", 300280000 }, 
			{"test_mrt_idzone", 300290000 }, 
			{"idarena", 300300000 }, 
			{"idraksha", 300310000 }, 
			{"idarena_solo", 300320000 }, 
			{"idldf4a", 300330000 }, 
			{"idarena_pvp01", 300350000 }, 
			{"idarena_pvp02", 300360000 }, 
			{"idldf4a_raid", 300380000 }, 
			{"idldf4a_lehpar", 300390000 }, 
			{"idldf4b_tiamat", 300400000 }, 
			{"idldf4a_intro", 300410000 }, 
			{"idarena_pvp01_t", 300420000 }, 
			{"idarena_pvp02_t", 300430000 }, 
			{"iddreadgion_03", 300440000 }, 
			{"idshulackship_solo", 300460000 }, 
			{"idtiamat_reward", 300470000 }, 
			{"idabprol1", 310010000 }, 
			{"idabprol2", 310020000 }, 
			{"idabgatel1", 310030000 }, 
			{"idabgatel2", 310040000 }, 
			{"idlf3lp", 310050000 }, 
			{"idlf1b", 310060000 }, 
			{"idlf1b_stigma", 310070000 }, 
			{"idlc1_arena", 310080000 }, 
			{"idlf3_castle_indratoo", 310090000 }, 
			{"idlf3_castle_lehpar", 310100000 }, 
			{"idlf2a_lab", 310110000 }, 
			{"idabprol3", 310120000 }, 
			{"idabprod1", 320010000 }, 
			{"idabprod2", 320020000 }, 
			{"idabgated1", 320030000 }, 
			{"idabgated2", 320040000 }, 
			{"iddf2flying", 320050000 }, 
			{"iddf1b", 320060000 }, 
			{"idspace", 320070000 }, 
			{"iddf3_dragon", 320080000 }, 
			{"iddc1_arena", 320090000 }, 
			{"iddf2_dflame", 320100000 }, 
			{"iddf3_lp", 320110000 }, 
			{"iddc1_arena_3f", 320120000 }, 
			{"iddf2a_adma", 320130000 }, 
			{"idabprod3", 320140000 }, 
			{"iddramata_01", 320150000 }, 
			{"ab1", 400010000 }, 
			{"lf_prison", 510010000 }, 
			{"df_prison", 520010000 }, 
			{"underpass", 600010000 }, 
			{"LDF4a", 600020000 }, 
			{"ldf4b", 600030000 }, 
			{"tiamat_down", 600040000 }, 
			{"housing_lf_personal", 700010000 }, 
			{"housing_lc_legion", 700020000 }, 
			{"housing_df_personal", 710010000 }, 
			{"housing_dc_legion", 710020000 }, 
			{"housing_idlf_personal", 720010000 }, 
			{"housing_iddf_personal", 730010000 }, 
			{"test_basic", 900020000 }, 
			{"test_server", 900030000 }, 
			{"test_giantmonster", 900100000 }, 
			{"housing_barrack", 900110000 }, 
			{"test_idarena", 900120000 }, 
        };
	}
}
