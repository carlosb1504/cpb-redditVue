import { shallowMount } from '@vue/test-utils'
import PostList from './../src/components/PostList.vue'

describe('PostList', () => {
    // Inspect the raw component options
    it('has data', () => {
        expect(typeof PostList.data).toBe('function')
    })
})

describe('Mounted PostList', () => {
    const wrapper = shallowMount(PostList, {
        propsData: {
            title: "my test title"
        }
    });

    test('is a Vue instance', () => {
        expect(wrapper.vm).toBeTruthy()
    })

    test('Has correct title set', async () => {
        const title = wrapper.find('#navbar-brand');

        expect(title.text()).toBe('my test title');
    })

})
