using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Zurhay
{
    internal class AlertListViewAdapter : BaseAdapter<string>
    {
        Activity _context = null;
        List<String> _mDivinity = null;

        public AlertListViewAdapter(Activity context, List<String> lstDataItem)
        {
            _context = context;
            _mDivinity = lstDataItem;
        }

        #region implemented abstract members of BaseAdapter

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
                convertView = _context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);

            (convertView.FindViewById<TextView>(Android.Resource.Id.Text1))
                .SetText(this[position], TextView.BufferType.Normal);

            return convertView;
        }

        public override int Count
        {
            get
            {
                return _mDivinity.Count;
            }
        }

        #endregion

        #region implemented abstract members of BaseAdapter

        public override string this[int index]
        {
            get
            {
                return _mDivinity[index];
            }
        }

        #endregion

    }
}

