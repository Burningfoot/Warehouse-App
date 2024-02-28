using DynamicData;
using Microsoft.Maui.Controls;
using ReactiveUI;
using SqliteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LagerApp.ViewModels.Pages
{
    public class ItemListViewModel : ReactiveObject
    {
        private readonly ApplicationDbContext _context;
        private readonly CompositeDisposable _disposables;

        private List<Package> itemList;

        public List<Package> ItemList
        {
            get => itemList;
            set => this.RaiseAndSetIfChanged(ref itemList, value);
        }

        private SqliteDB.Area tempArea;

        public SqliteDB.Area TempArea
        {
            get => tempArea;
            set => this.RaiseAndSetIfChanged(ref tempArea, value);
        }

        private IEnumerable<Package> packagesForTransport;

        public IEnumerable<Package> PackagesForTransport
        {
            get => packagesForTransport;
            set => this.RaiseAndSetIfChanged(ref packagesForTransport, value);
        }

        private SqliteDB.Branch branchInArea;

        public SqliteDB.Branch BranchInArea
        {
            get => branchInArea;
            set => this.RaiseAndSetIfChanged(ref branchInArea, value);
        }

        private SqliteDB.Item itemOfBranch;

        public SqliteDB.Item ItemOfBranch
        {
            get => itemOfBranch;
            set => this.RaiseAndSetIfChanged(ref itemOfBranch, value);
        }



        public ReactiveCommand<string, SqliteDB.Area> ScanArea1Command { get; set; }
        public ReactiveCommand<string, Unit> ScanQrCommand { get; set; }

        public ItemListViewModel(ApplicationDbContext context)
        {
            _context = context;
            _disposables = new CompositeDisposable();
            ItemList = new List<Package>();
            PackagesForTransport = new ObservableCollection<Package>();

            ScanArea1Command = ReactiveCommand.CreateFromTask<string, Area>(async (code) =>
            {
                if (_context.Set<Area>().Where(w => w.Code == code).Count() == 0)
                {
                    var newArea = new Area();
                    newArea.Code = code;
                    newArea.Name = code;
                    newArea.Capacity = 10;
                    _context.Add(newArea);
                    await _context.SaveChangesAsync();
                }
                var area = _context.Set<Area>().Where(w => w.Code == code).First();
                TempArea = area;
                //TestDbPack = _context.Packages.Where(w => w.Area == TempArea).Select(s => s).ToList();
                BranchInArea = _context.Set<Branch>().Where(w => w.Area == TempArea).Select(s => s).FirstOrDefault();
                ItemOfBranch = _context.Set<Branch>().Where(w => w.Area == TempArea).Select(s => s.Item).FirstOrDefault();
                return area;
            }).DisposeWith(_disposables);

            ScanQrCommand = ReactiveCommand.CreateFromTask<string>(async (code) =>
            {
                string[] codeArray = code.Split(';');
                if (codeArray[0] != "Branch")
                {
                    if (_context.Set<Package>().Where(w => w.Id == Guid.Parse(codeArray[0])).Count() == 0)
                    {
                        if (TempArea != null)
                        {
                            var newPackage = new Package();
                            newPackage.Id = Guid.Parse(codeArray[0].Replace("-", ""));
                            newPackage.Area = TempArea;
                            newPackage.Branch = _context.Set<SqliteDB.Branch>().Where(w => w.Area == TempArea).Select(s => s).FirstOrDefault();
                            newPackage.Amount = int.Parse(codeArray[3]);
                            var packageInDb = _context.Add(newPackage).Entity;
                            await _context.SaveChangesAsync();
                            //var x = _context.Set<SqliteDB.Branch>().Where(w => w.Area.Id == tempArea.Id).FirstOrDefault();
                            var tempList = PackagesForTransport.ToList();
                            tempList.Add(packageInDb);
                            PackagesForTransport = tempList.ToList();
                        }

                    }
                    else
                    {
                        var packageInDb = _context.Set<Package>().Where(w => w.Id == Guid.Parse(codeArray[0])).SingleOrDefault();
                        var branchOfPackage = _context.Set<Package>().Where(w => w.Id == packageInDb.Id).Select(s => s.Branch).FirstOrDefault();
                        var areaOfPackage = _context.Set<Package>().Where(w => w.Id == packageInDb.Id).Select(s => s.Area).FirstOrDefault();
                        packageInDb.Area = areaOfPackage;
                        packageInDb.Branch = branchOfPackage;
                        var tempList = PackagesForTransport.ToList();
                        tempList.Add(packageInDb);
                        PackagesForTransport = tempList.ToList();


                    }

                }
                else
                {
                    if (_context.Set<Branch>().Where(w => w.Id == Guid.Parse(codeArray[1])).Count() == 0)
                    {
                        var newItem = new Item();
                        newItem.Name = codeArray[3];
                        newItem.Quantity = "Piece";
                        newItem.Producer = codeArray[2];
                        newItem.Description = "Description";
                        var itemInDb = _context.Add(newItem).Entity;


                        var newBranch = new Branch();
                        newBranch.Id = Guid.Parse(codeArray[1].Replace("-", ""));
                        newBranch.Area = TempArea;
                        newBranch.ExpiredDate = DateTime.ParseExact(codeArray[8], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        newBranch.Item = itemInDb;
                        newBranch.Name = "Neuer Branch";
                        newBranch.Capacity = int.Parse(codeArray[5]);

                        _context.Add(newBranch);
                        await _context.SaveChangesAsync();
                    }
                }
                //var package = _context.Set<Package>().Where(w => w.Id == Guid.Parse(codeArray[0])).Single();
                //ItemList.Add(package);
            }).DisposeWith(_disposables);
        }



    }
}
