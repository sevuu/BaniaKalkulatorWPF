using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace BaniaKalkulatorLibrary
{
    public class Beverage
    {
        public string _nazwa;
        private double _ml, _procent, _cena, _spejson, _etanol, tmpprocent;
        public Beverage()
        {
            _nazwa = string.Empty;
            _ml = 0.0;
            _procent = 0.0;
            _cena = 0.0;
            _spejson = 0.0;
            _etanol = 0.0;
            tmpprocent = 0.0;
        }
        public Beverage(string nazwa, double ml, double procent, double cena, double spejson, double etanol)
        {
            _nazwa = nazwa;
            _ml = ml;
            _procent = procent;
            _cena = cena;
            _spejson = spejson;
            _etanol = etanol;
        }

        public void Spejsonowanie()
        {
            tmpprocent = _procent;
            if (_ml != 500.0)
            {
                double podzial = 500.0 / _ml;
                tmpprocent = tmpprocent / podzial;
            }
            double wspolczynnik = tmpprocent / _cena;
            _spejson = double.Round(wspolczynnik, 3);
        }

        public void Etanolowanie()
        {
            double realprocent = tmpprocent / 100;
            double ilealko = realprocent * _ml;
            if (_ml != 500.0)
            {                   
                double podzial = 500.0 / _ml;
                ilealko = ilealko * podzial;
            }
           _etanol = double.Round(ilealko,3);
        }

        public string Nazwa
        {
            get { return _nazwa; }
            set { _nazwa = value; }
        }
        public double Ml
        {
            get { return _ml; }
            set { _ml = value; }
        }
        public double Procent
        {
            get { return _procent; }
            set { _procent = value; }
        }
        public double Cena
        {
            get { return _cena; }
            set { _cena = value; }
        }
        public double Spejson
        {
            get { return _spejson; }
            set { _spejson = value; }
        }
        public double Etanol
        {
            get { return _etanol; }
            set { _etanol = value; }
        }
    }
}
