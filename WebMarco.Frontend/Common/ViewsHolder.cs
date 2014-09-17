using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebMarco.Frontend.Common {
    public class ViewsHolder {

        private Stack<IBaseView> views;

        /// <summary>
        /// Contains all views that will be shown\called from current form
        /// </summary>
        public List<IBaseView> Items {
            get {
                return views.ToList<IBaseView>();
            }
        }

        public IBaseView TopView {
            get {
                return views.Last();
            }
        }

        public void TryAddView(IBaseView view) {

            //TODO: make this work.
            //if (view.IsModal == false) {
            //    if (views.Count >= 2) {
            //        views.Pop();
            //    }
            //}

            views.Push(view);
        }

        public List<IBaseView> ViewForEachType {
            get {
                List<IBaseView> result;
                if (Items != null)
                    result = Items.ToList<IBaseView>();
                else
                    result = new List<IBaseView>();
                return result;
            }
        }

        /// <summary>
        /// Determines if any view of provided type is already loaded
        /// </summary>
        /// <param name="viewType"></param>
        /// <returns></returns>
        //public bool IsViewTypeLoaded (ApplicationView viewType) {
        //    return mLoadedViews.ContainsKey(viewType);
        //}

        public ViewsHolder() {
            views = new Stack<IBaseView>();
        }
    }
}
