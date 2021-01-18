using System.Linq;
using System.Collections.Generic;
using Monitor.Domain.Entities;

namespace Monitor.Infrastructure.SeedHelpers
{
    public static class LocalGovernmentAreasHelper
    {
        public static void Seed(MinigridDbContext context)
        {
            if (context.LocalGovernmentAreas.Any())
            {
                return;
            }

            var states = context.States.ToList();

            var areas = new string[]
            {
                "Aba North",
                "Aba South",
                "Arochukwu",
                "Bende",
                "Ikwuano",
                "Isiala-Ngwa North",
                "Isiala-Ngwa South",
                "Isuikwuato",
                "Obi Ngwa",
                "Ohafia",
                "Osisioma Ngwa",
                "Ugwunagbo",
                "Ukwa East",
                "Ukwa West",
                "Umuahia North",
                "Umuahia South",
                "Umu-Nneochi"
            };

            CreateAreas(context, areas, states.First(x => x.Name == "Abia").Id);

            areas = new string[]
            {
                "Demsa",
                "Fufore",
                "Ganye",
                "Girei",
                "Gombi",
                "Guyuk",
                "Hong",
                "Jada",
                "Lamurde",
                "Madagali",
                "Maiha",
                "Mayo-Belwa",
                "Michika",
                "Mubi North",
                "Mubi South",
                "Numan",
                "Shelleng",
                "Song",
                "Toungo",
                "Yola North",
                "Yola South"
            };

            CreateAreas(context, areas, states.First(x => x.Name == "Adamawa").Id);

            areas = new string[]
            {
                "Abak",
                "Eastern Obolo",
                "Eket",
                "Esit Eket",
                "Essien Udim",
                "Etim Ekpo",
                "Etinan",
                "Ibeno",
                "Ibesikpo Asutan",
                "Ibiono Ibom",
                "Ika",
                "Ikono",
                "Ikot Abasi",
                "Ikot Ekpene",
                "Ini",
                "Itu",
                "Mbo",
                "Mkpat Enin",
                "Nsit Atai",
                "Nsit Ibom",
                "Nsit Ubium",
                "Obot Akara",
                "Okobo",
                "Onna",
                "Oron",
                "Oruk Anam",
                "Udung Uko",
                "Ukanafun",
                "Uruan",
                "Urue-Offong/Oruko",
                "Uyo"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Akwa Ibom").Id);

            areas = new string[]
            {
                "Aguata",
                "Anambra East",
                "Anambra West",
                "Anaocha",
                "Awka North",
                "Awka South",
                "Ayamelum",
                "Dunukofia",
                "Ekwusigo",
                "Idemili North",
                "Idemili South",
                "Ihiala",
                "Njikoka",
                "Nnewi North",
                "Nnewi South",
                "Ogbaru",
                "Onitsha North",
                "Onitsha South",
                "Orumba North",
                "Orumba South",
                "Oyi"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Anambra").Id);

            areas = new string[]
            {
                "Alkaleri",
                "Bauchi",
                "Bogoro",
                "Damban",
                "Darazo",
                "Dass",
                "Gamawa",
                "Ganjuwa",
                "Giade",
                "Itas/Gadau",
                "Jama'are",
                "Katagum",
                "Kirfi",
                "Misau",
                "Ningi",
                "Shira",
                "Tafawa-Balewa",
                "Toro",
                "Warji",
                "Zaki"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Bauchi").Id);

            areas = new string[]
            {
                "Brass",
                "Ekeremor",
                "Kolokuma/Opokuma",
                "Nembe",
                "Ogbia",
                "Sagbama",
                "Southern Ijaw",
                "Yenagoa"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Bayelsa").Id);

            areas = new string[]
            {
                "Ado",
                "Agatu",
                "Apa",
                "Buruku",
                "Gboko",
                "Guma",
                "Gwer East",
                "Gwer West",
                "Katsina-Ala",
                "Konshisha",
                "Kwande",
                "Logo",
                "Makurdi",
                "Obi",
                "Ogbadibo",
                "Ohimini",
                "Oju",
                "Okpokwu",
                "Oturkpo",
                "Tarka",
                "Ukum",
                "Ushongo",
                "Vandeikya"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Benue").Id);

            areas = new string[]
            {
                "Abadam",
                "Askira/Uba",
                "Bama",
                "Bayo",
                "Biu",
                "Chibok",
                "Damboa",
                "Dikwa",
                "Gubio",
                "Guzamala",
                "Gwoza",
                "Hawul",
                "Jere",
                "Kaga",
                "Kala/Balge",
                "Konduga",
                "Kukawa",
                "Kwaya Kusar",
                "Mafa",
                "Magumeri",
                "Maiduguri",
                "Marte",
                "Mobbar",
                "Monguno",
                "Ngala",
                "Nganzai",
                "Shani"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Borno").Id);

            areas = new string[]
            {
                "Abi",
                "Akamkpa",
                "Akpabuyo",
                "Bakassi",
                "Bekwara",
                "Biase",
                "Boki",
                "Calabar Municipal",
                "Calabar South",
                "Etung",
                "Ikom",
                "Obanliku",
                "Obubra",
                "Obudu",
                "Odukpani",
                "Ogoja",
                "Yakurr",
                "Yala"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Cross River").Id);

            areas = new string[]
            {
                "Aniocha North",
                "Aniocha South",
                "Bomadi",
                "Burutu",
                "Ethiope East",
                "Ethiope West",
                "Ika North East",
                "Ika South",
                "Isoko North",
                "Isoko South",
                "Ndokwa East",
                "Ndokwa West",
                "Okpe",
                "Oshimili North",
                "Oshimili South",
                "Patani",
                "Sapele",
                "Udu",
                "Ughelli North",
                "Ughelli South",
                "Ukwuani",
                "Uvwie",
                "Warri North",
                "Warri South",
                "Warri South West"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Delta").Id);

            areas = new string[]
            {
                "Abakaliki",
                "Afikpo North",
                "Afikpo South",
                "Ebonyi",
                "Ezza North",
                "Ezza South",
                "Ikwo",
                "Ishielu",
                "Ivo",
                "Izzi",
                "Ohaozara",
                "Ohaukwu",
                "Onicha"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Ebonyi").Id);

            areas = new string[]
            {
                "Akoko-Edo",
                "Egor",
                "Esan Central",
                "Esan North East",
                "Esan South East",
                "Esan West",
                "Etsako Central",
                "Etsako East",
                "Etsako West",
                "Igueben",
                "Ikpoba-Okha",
                "Oredo",
                "Orhionmwon",
                "Ovia North East",
                "Ovia South West",
                "Owan East",
                "Owan West",
                "Uhunmwonde"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Edo").Id);

            areas = new string[]
            {
                "Ado Ekiti",
                "Gbonyin",
                "Efon",
                "Ekiti East",
                "Ekiti South West",
                "Ekiti West",
                "Emure",
                "Ido-Osi",
                "Ijero",
                "Ikere",
                "Ikole",
                "Ilejemeje",
                "Irepodun/Ifelodun",
                "Ise/Orun",
                "Moba",
                "Oye"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Ekiti").Id);

            areas = new string[]
            {
                "Aninri",
                "Awgu",
                "Enugu East",
                "Enugu North",
                "Enugu South",
                "Ezeagu",
                "Igbo-Etiti",
                "Igbo-Eze North",
                "Igbo-Eze South",
                "Isi-Uzo",
                "Nkanu East",
                "Nkanu West",
                "Nsukka",
                "Oji-River",
                "Udenu",
                "Udi",
                "Uzo-Uwani"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Enugu").Id);

            areas = new string[]
            {
                "Abaji",
                "Abuja Municipal Area Council",
                "Bwari",
                "Gwagwalada",
                "Kuje",
                "Kwali"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Federal Capital Territory").Id);

            areas = new string[]
            {
                "Akko",
                "Balanga",
                "Billiri",
                "Dukku",
                "Funakaye",
                "Gombe",
                "Kaltungo",
                "Kwami",
                "Nafada",
                "Shomgom",
                "Yamaltu/Deba"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Gombe").Id);

            areas = new string[]
            {
                "Abo-Mbaise",
                "Ahiazu-Mbaise",
                "Ehime-Mbano",
                "Ezinihitte",
                "Ideato North",
                "Ideato South",
                "Ihitte/Uboma",
                "Ikeduru",
                "Isiala-Mbano",
                "Isu",
                "Mbaitoli",
                "Ngor-Okpala",
                "Njaba",
                "Nkwerre",
                "Nwangele",
                "Obowo",
                "Oguta",
                "Ohaji/Egbema",
                "Okigwe",
                "Orlu",
                "Orsu",
                "Oru East",
                "Oru West",
                "Owerri Municipal",
                "Owerri North",
                "Owerri West",
                "Unuimo"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Imo").Id);

            areas = new string[]
            {
                "Auyo",
                "Babura",
                "Biriniwa",
                "Birnin Kudu",
                "Buji",
                "Dutse",
                "Gagarawa",
                "Garki",
                "Gumel",
                "Guri",
                "Gwaram",
                "Gwiwa",
                "Hadejia",
                "Jahun",
                "Kafin Hausa",
                "Kaugama",
                "Kazaure",
                "Kiri Kasama",
                "Kiyawa",
                "Maigatari",
                "Malam Madori",
                "Miga",
                "Ringim",
                "Roni",
                "Sule Tankarkar",
                "Taura",
                "Yankwashi"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Jigawa").Id);

            areas = new string[]
            {
                "Birnin-Gwari",
                "Chikun",
                "Giwa",
                "Igabi",
                "Ikara",
                "Jaba",
                "Jema'a",
                "Kachia",
                "Kaduna North",
                "Kaduna South",
                "Kagarko",
                "Kajuru",
                "Kaura",
                "Kauru",
                "Kubau",
                "Lere",
                "Makarfi",
                "Sabon-Gari",
                "Sanga",
                "Soba",
                "Zangon-Kataf",
                "Zaria"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Kaduna").Id);

            areas = new string[]
            {
                "Ajingi",
                "Albasu",
                "Bagwai",
                "Bebeji",
                "Bichi",
                "Bunkure",
                "Dala",
                "Dambatta",
                "Dawakin Kudu",
                "Dawakin Tofa",
                "Doguwa",
                "Fagge",
                "Gabasawa",
                "Garko",
                "Garum Mallam",
                "Gaya",
                "Gezawa",
                "Gwale",
                "Gwarzo",
                "Kabo",
                "Kano Municipal",
                "Karaye",
                "Kibiya",
                "Kiru",
                "Kumbotso",
                "Kunchi",
                "Kura",
                "Madobi",
                "Makoda",
                "Minjibir",
                "Nasarawa",
                "Rano",
                "Rimin Gado",
                "Rogo",
                "Shanono",
                "Sumaila",
                "Takai",
                "Tarauni",
                "Tofa",
                "Tsanyawa",
                "Tudun Wada",
                "Ungogo",
                "Warawa",
                "Wudil"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Kano").Id);

            areas = new string[]
            {
                "Bakori",
                "Batagarawa",
                "Batsari",
                "Baure",
                "Bindawa",
                "Charanchi",
                "Dandume",
                "Danja",
                "Dan Musa",
                "Daura",
                "Dutsi",
                "Dutsin-Ma",
                "Faskari",
                "Funtua",
                "Ingawa",
                "Jibia",
                "Kafur",
                "Kaita",
                "Kankara",
                "Kankia",
                "Katsina",
                "Kurfi",
                "Kusada",
                "Mai'adua",
                "Malumfashi",
                "Mani",
                "Mashi",
                "Matazu",
                "Musawa",
                "Rimi",
                "Sabuwa",
                "Safana",
                "Sandamu",
                "Zango"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Katsina").Id);

            areas = new string[]
            {
                "Aleiro",
                "Arewa-Dandi",
                "Argungu",
                "Augie",
                "Bagudo",
                "Birnin Kebbi",
                "Bunza",
                "Dandi",
                "Fakai",
                "Gwandu",
                "Jega",
                "Kalgo",
                "Koko/Besse",
                "Maiyama",
                "Ngaski",
                "Sakaba",
                "Shanga",
                "Suru",
                "Wasagu/Danko",
                "Yauri",
                "Zuru"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Kebbi").Id);

            areas = new string[]
            {
                "Adavi",
                "Ajaokuta",
                "Ankpa",
                "Bassa",
                "Dekina",
                "Ibaji",
                "Idah",
                "Igalamela-Odolu",
                "Ijumu",
                "Kabba/Bunu",
                "Kogi",
                "Lokoja",
                "Mopa-Muro",
                "Ofu",
                "Ogori/Magongo",
                "Okehi",
                "Okene",
                "Olamaboro",
                "Omala",
                "Yagba East",
                "Yagba West",
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Kogi").Id);

            areas = new string[]
            {
                "Asa",
                "Baruten",
                "Edu",
                "Ekiti",
                "Ifelodun",
                "Ilorin East",
                "Ilorin South",
                "Ilorin West",
                "Irepodun",
                "Isin",
                "Kaiama",
                "Moro",
                "Offa",
                "Oke-Ero",
                "Oyun",
                "Pategi",
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Kwara").Id);

            areas = new string[]
            {
                "Agege",
                "Ajeromi-Ifelodun",
                "Alimosho",
                "Amuwo-Odofin",
                "Apapa",
                "Badagry",
                "Epe",
                "Eti-Osa",
                "Ibeju/Lekki",
                "Ifako-Ijaye",
                "Ikeja",
                "Ikorodu",
                "Kosofe",
                "Lagos Island",
                "Lagos Mainland",
                "Mushin",
                "Ojo",
                "Oshodi-Isolo",
                "Shomolu",
                "Surulere"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Lagos").Id);

            areas = new string[]
            {
                "Akwanga",
                "Awe",
                "Doma",
                "Karu",
                "Keana",
                "Keffi",
                "Kokona",
                "Lafia",
                "Nasarawa",
                "Nasarawa-Eggon",
                "Obi",
                "Toto",
                "Wamba",
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Nasarawa").Id);

            areas = new string[]
            {
                "Agaie",
                "Agwara",
                "Bida",
                "Borgu",
                "Bosso",
                "Chanchaga",
                "Edati",
                "Gbako",
                "Gurara",
                "Katcha",
                "Kontagora",
                "Lapai",
                "Lavun",
                "Magama",
                "Mariga",
                "Mashegu",
                "Mokwa",
                "Muya",
                "Paikoro",
                "Rafi",
                "Rijau",
                "Shiroro",
                "Suleja",
                "Tafa",
                "Wushishi",
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Niger").Id);

            areas = new string[]
            {
                "Abeokuta North",
                "Abeokuta South",
                "Ado-Odo/Ota",
                "Ewekoro",
                "Ifo",
                "Ijebu East",
                "Ijebu North",
                "Ijebu North East",
                "Ijebu Ode",
                "Ikenne",
                "Imeko Afon",
                "Ipokia",
                "Obafemi-Owode",
                "Odeda",
                "Odogbolu",
                "Ogun Waterside",
                "Remo North",
                "Sagamu",
                "Yewa North",
                "Yewa South",
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Ogun").Id);

            areas = new string[]
            {
                "Akoko North East",
                "Akoko North West",
                "Akoko South East",
                "Akoko South West",
                "Akure North",
                "Akure South",
                "Ese-Odo",
                "Idanre",
                "Ifedore",
                "Ilaje",
                "Ile-Oluji-Okeigbo",
                "Irele",
                "Odigbo",
                "Okitipupa",
                "Ondo East",
                "Ondo West",
                "Ose",
                "Owo",
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Ondo").Id);

            areas = new string[]
            {
                "Aiyedaade",
                "Aiyedire",
                "Atakunmosa East",
                "Atakunmosa West",
                "Boluwaduro",
                "Boripe",
                "Ede North",
                "Ede South",
                "Egbedore",
                "Ejigbo",
                "Ife Central",
                "Ifedayo",
                "Ife East",
                "Ifelodun",
                "Ife North",
                "Ife South",
                "Ila",
                "Ilesha East",
                "Ilesha West",
                "Irepodun",
                "Irewole",
                "Isokan",
                "Iwo",
                "Obokun",
                "Odo-Otin",
                "Ola-Oluwa",
                "Olorunda",
                "Oriade",
                "Orolu",
                "Osogbo",
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Osun").Id);

            areas = new string[]
            {
                "Afijio",
                "Akinyele",
                "Atiba",
                "Atisbo",
                "Egbeda",
                "Ibadan North",
                "Ibadan North East",
                "Ibadan North West",
                "Ibadan South East",
                "Ibadan South West",
                "Ibarapa Central",
                "Ibarapa East",
                "Ibarapa North",
                "Ido",
                "Irepo",
                "Iseyin",
                "Itesiwaju",
                "Iwajowa",
                "Kajola",
                "Lagelu",
                "Ogbomosho North",
                "Ogbomosho South",
                "Ogo Oluwa",
                "Olorunsogo",
                "Oluyole",
                "Ona-Ara",
                "Orelope",
                "Ori Ire",
                "Oyo East",
                "Oyo West",
                "Saki East",
                "Saki West",
                "Surulere"
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Oyo").Id);

            areas = new string[]
            {
                "Barkin Ladi",
                "Bassa",
                "Bokkos",
                "Jos East",
                "Jos North",
                "Jos South",
                "Kanam",
                "Kanke",
                "Langtang North",
                "Langtang South",
                "Mangu",
                "Mikang",
                "Pankshin",
                "Qua'an Pan",
                "Riyom",
                "Shendam",
                "Wase",
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Plateau").Id);

            areas = new string[]
            {
                "Abua - Odual",
                "Ahoada East",
                "Ahoada West",
                "Akuku Toru",
                "Andoni",
                "Asari-Toru",
                "Bonny",
                "Degema",
                "Eleme",
                "Emuoha",
                "Etche",
                "Gokana",
                "Ikwerre",
                "Khana",
                "Obio/Akpor",
                "Ogba - Egbema - Ndoni",
                "Ogu - Bolo",
                "Okrika",
                "Omumma",
                "Opobo - Nkoro",
                "Oyigbo",
                "Port-Harcourt",
                "Tai",
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Rivers").Id);

            areas = new string[]
            {
                "Binji",
                "Bodinga",
                "Dange Shuni",
                "Gada",
                "Goronyo",
                "Gudu",
                "Gwadabawa",
                "Illela",
                "Isa",
                "Kebbe",
                "Kware",
                "Rabah",
                "Sabon Birni",
                "Shagari",
                "Silame",
                "Sokoto North",
                "Sokoto South",
                "Tambuwal",
                "Tangaza",
                "Tureta",
                "Wamakko",
                "Wurno",
                "Yabo",
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Sokoto").Id);

            areas = new string[]
            {
                "Ardo-Kola",
                "Bali",
                "Disputed Areas",
                "Donga",
                "Gashaka",
                "Gassol",
                "Ibi",
                "Jalingo",
                "Karim-Lamido",
                "Kurmi",
                "Lau",
                "Sardauna",
                "Takum",
                "Ussa",
                "Wukari",
                "Yorro",
                "Zing",
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Taraba").Id);

            areas = new string[]
            {
                "Bade",
                "Bursari",
                "Damaturu",
                "Fika",
                "Fune",
                "Geidam",
                "Gujba",
                "Gulani",
                "Jakusko",
                "Karasuwa",
                "Machina",
                "Nangere",
                "Nguru",
                "Potiskum",
                "Tarmua",
                "Yunusari",
                "Yusufari",
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Yobe").Id);

            areas = new string[]
            {
                "Anka",
                "Bakura",
                "Birnin Magaji",
                "Bukkuyum",
                "Bungudu",
                "Gummi",
                "Gusau",
                "Kaura Namoda",
                "Maradun",
                "Maru",
                "Shinkafi",
                "Talata Mafara",
                "Tsafe",
                "Zurmi",
            };
            CreateAreas(context, areas, states.First(x => x.Name == "Zamfara").Id);
        }

        private static void CreateAreas(MinigridDbContext context, string[] names, int stateId)
        {
            var areas = new List<LocalGovernmentArea>();

            foreach (var name in names)
            {
                var entity = new LocalGovernmentArea();
                entity.Set(name, stateId);

                areas.Add(entity);
            }

            context.LocalGovernmentAreas.AddRange(areas);
            context.SaveChanges();
        }
    }
}
