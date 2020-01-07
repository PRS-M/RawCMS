const _LogsView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl('/modules/core/views/logs-view/logs-view.tpl.html');

  res({
    data: function() {
      return {
        search: '',
      };
    },
    template: tpl,
  });
};

export const LogsView = _LogsView;
export default _LogsView;
