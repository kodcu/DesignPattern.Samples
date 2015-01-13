using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod.Sample4
{
    class Program
    {
        static void Main(string[] args)
        {
            // kullanımı
            AccountingFlow flow = new AccountingFlow();

            flow.Process(new ForeignAccountingFactory());
            flow.Process(new DomesticAccountingFactory());            

            Console.Read();
        }
    }

    public interface IAccounting 
    {
        void ExecuteAccountingControl();
        void ExecuteAccounting();
    }

    public abstract class BaseAccounting
        : IAccounting
    {
        public abstract void BeforeCommitAccounting();
        public abstract void AfterCommitAccounting();

        public virtual void ExecuteAccountingControl()
        { 
            // temel fonksiyonlar
        }

        public virtual void ExecuteAccounting() 
        {
            // temel fonksiyonlar
        }
    }

    public class ForeignAccounting
        : BaseAccounting
    {

        public override void BeforeCommitAccounting()
        {
            throw new NotImplementedException();
        }

        public override void AfterCommitAccounting()
        {
            throw new NotImplementedException();
        }

        public override void ExecuteAccounting()
        {
            // ek fonksiyonellikler
            base.ExecuteAccounting();
        }
    }

    public class DomestricAccounting
        : BaseAccounting
    {

        public override void BeforeCommitAccounting()
        {
            throw new NotImplementedException();
        }

        public override void AfterCommitAccounting()
        {
            throw new NotImplementedException();
        }
    }

    // factory tasarımı
    // sınıfların sonekini creator olarak da isimlendirebiliriz
    public interface IAccountingFactory
    {
        BaseAccounting Create();
    }

    public class ForeignAccountingFactory
        : IAccountingFactory
    {
        private ForeignAccounting ForeignAccounting
        {
            get
            {
                return new ForeignAccounting();
            }
        }
    
        public BaseAccounting Create()
        {
            return ForeignAccounting;
        }
    }

    public class DomesticAccountingFactory
        : IAccountingFactory
    {
        private DomestricAccounting DomestricAccounting
        {
            get
            {
                return new DomestricAccounting();
            }
        }
    
        public BaseAccounting Create()
        {
            return DomestricAccounting;
        }
    }

    public class AccountingFlow
    {
        public void Process(IAccountingFactory factory)
        {
            BaseAccounting accounting = factory.Create();

            // simgesel işlemler
            accounting.ExecuteAccountingControl();
            accounting.ExecuteAccounting();
            accounting.BeforeCommitAccounting();
            accounting.AfterCommitAccounting();
        }
    }   
}
